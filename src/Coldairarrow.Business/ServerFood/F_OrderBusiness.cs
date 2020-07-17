using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Coldairarrow.Entity.Base_Manage;
using CSRedis;
using Hangfire;
using Quartz.Impl.Matchers;
using StackExchange.Redis;

namespace Coldairarrow.Business.ServerFood
{
    public class F_OrderBusiness : BaseBusiness<F_Order>, IF_OrderBusiness, ITransientDependency
    {
        public IOperator oOperator;
        private static readonly object SequenceLock = new object();
        public F_OrderBusiness(IRepository repository, IOperator op)
            : base(repository)
        {
            oOperator = op;
        }

        #region 外部接口

        public async Task<PageResult<IF_OrderResultDTO>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            Expression<Func<F_Order, F_UserInfo,Base_DepartmentRelation,F_PublishFood,Base_Dictionary, IF_OrderResultDTO >> select = (a, b,c,d,f) => new IF_OrderResultDTO
            {
                UserName = b.UserName,
                OldDepartmentName = c.OldDepartment,
                FoodName =d.FoodName,
                StatusName = f.DicDisplayValue
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                join e in Service.GetIQueryable<F_OrderInfo>() on a.OrderCode equals e.OrderCode
                join f in  Service.GetIQueryable<F_PublishFood>() on e.PublishFoodId equals  f.Id
                join b in Service.GetIQueryable<F_UserInfo>() on a.UserInfoId equals b.Id into ab
                from b in ab.DefaultIfEmpty()
                join c  in  Service.GetIQueryable<Base_DepartmentRelation>() on b.FullDepartment equals  c.Department into bc
                from c in bc.DefaultIfEmpty()
                join g  in Service.GetIQueryable<Base_Dictionary>() on  new { Status = a.Status.ToString(), OrderStatus = "OrderStatus" } equals new { Status = g.DicValue, OrderStatus = g.DicKey} into ag
                from h in ag.DefaultIfEmpty()
                select @select.Invoke(a, b,c,f,h);
            var where = LinqHelper.True<IF_OrderResultDTO>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<IF_OrderResultDTO, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).OrderByDescending(a=>a.CreateTime).GetPageResultAsync(input);
        }


        public async Task<List<IF_OrderResultDTO>> GetDataListToMoblieAsync()
        {
            Expression<Func<F_Order, F_UserInfo, IF_OrderResultDTO>> select = (a, b) => new IF_OrderResultDTO
            {
                UserName = b.UserName,
                ImageUrl  = (from c in Service.GetIQueryable<F_OrderInfo>()
                    join d in Service.GetIQueryable<F_PublishFood>() on c.PublishFoodId equals d.Id
                    where c.OrderCode == a.OrderCode
                    select d.ImgUrl).FirstOrDefault()

            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                    join b in Service.GetIQueryable<F_UserInfo>() on a.UserInfoId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    select @select.Invoke(a, b);
           var where = LinqHelper.True<IF_OrderResultDTO>();
           var userInfo = Service.GetIQueryable<F_UserInfo>().Where(a => a.WeCharUserId ==oOperator.UserId)?.FirstOrDefault();
           if (userInfo == null) throw new BusException("获取用户信息失败!");
            //筛选
           where = where.And(a=>a.UserInfoId== userInfo.Id);
           var queryDic = await Service.GetIQueryable<Base_Dictionary>().ToListAsync();

           var listPage = await q.Where(where).OrderByDescending(a => a.CreateTime).ToListAsync();
           listPage.ForEach(a =>
           {
               a.StatusName = queryDic.FirstOrDefault(b => b.DicKey == "OrderStatus" && b.DicValue == a.Status.ToString())?.DicDisplayValue;
           });
           return listPage;
        }

        public async Task PlaceOrderAsync(List<IF_OrderInputDTO> data)
        {
            if (data == null || data.Count == 0)
            {
                throw new BusException("数据不正常请检查!");
            }
            var userInfo = (from a in Service.GetIQueryable<F_UserInfo>()
                join b in Service.GetIQueryable<Base_DepartmentRelation>() on a.FullDepartment equals b.Department into ab
                from b in ab.DefaultIfEmpty()
                where a.WeCharUserId == oOperator.UserId
                select new
                {
                    ShopInfoId=a.ShopInfoId,
                    Id=a.Id,
                    UserName=a.UserName,
                    OldDepartment = b.OldDepartment,
                    FullDepartment= a.FullDepartment
                }).FirstOrDefault();
            if (userInfo == null) throw new BusException("获取用户信息失败!");
            if (string.IsNullOrEmpty(userInfo.ShopInfoId)) throw new BusException("请先到[我的]绑定门店!");
            //加锁防止并发
            lock (SequenceLock)
            {   
                //查询发布菜品信息
                var fPublishFoodList = Service.GetIQueryable<F_PublishFood>()
                    .Where(a => data.Select(b => b.Id).Contains(a.Id)).ToList();
                if (fPublishFoodList.Count == 0) throw new BusException("数据查询错误!");
                var fShopInfoSet = Service.GetIQueryable<F_ShopInfoSet>()
                    .Where(a => a.ShopInfoId == fPublishFoodList.FirstOrDefault().ShopInfoId).FirstOrDefault();
                if (fShopInfoSet == null) throw new BusException("门店设置异常!");
                if (fShopInfoSet != null && fShopInfoSet.OrderBeginDate.HasValue && fShopInfoSet.OrderBeginEnd.HasValue)
                {
                    var BeginDate = fShopInfoSet.OrderBeginDate.Value.ToString("HHmm").ToInt();
                    var BeginEnd = fShopInfoSet.OrderBeginEnd.Value.ToString("HHmm").ToInt();
                    var toDay = DateTime.Now.ToString("HHmm").ToInt();
                    if (toDay < BeginDate || toDay > BeginEnd) throw new BusException("目前不是点餐时间!");
                }

                if (fPublishFoodList.Any(a => a.FoodQty <= 0))
                {
                    throw new BusException("商品已经售罄请重新选择!");
                }

                //检查限购
                fPublishFoodList.ForEach(a =>
                {
                    if (a.Limit.HasValue)
                    {
                        if (data.Any(b => b.Id == a.Id && b.Num > a.Limit))
                        {
                            throw new BusException("商品[" + a.FoodName + "]限购" + a.Limit + "件");
                        }
                    }
                });
                //检查订单SKU数量
                if (fShopInfoSet.OrderFoodTypeNum.HasValue)
                {
                    if (data.Count > fShopInfoSet.OrderFoodTypeNum)
                    {
                        throw new BusException("订单只能选择" + fShopInfoSet.OrderFoodTypeNum + "种商品");
                    }
                } //检查当天订单数量

                if (fShopInfoSet.UserOrderNum.HasValue)
                {
                    var toDay = DateTime.Now.Date;
                    var taDayOrderNum = Service.GetIQueryable<F_Order>().Count(a =>
                        a.UserInfoId == userInfo.Id && a.CreateTime >= toDay && a.CreateTime < toDay.AddDays(1) &&
                        a.Status != 4);
                    if (taDayOrderNum >= fShopInfoSet.UserOrderNum)
                        throw new BusException("今天已下单" + taDayOrderNum + "个,不能再下单");
                }

                //扣减商品数量
                fPublishFoodList.ForEach(a =>
                {

                    a.FoodQty = a.FoodQty - data.First(b => b.Id == a.Id).Num;
                    if (a.FoodQty < 0)
                    {
                        throw new BusException("商品数量不足请刷新页面重试!");
                    }
                });
                Service.UpdateAny<F_PublishFood>(fPublishFoodList, new List<string>() {"FoodQty"});
                //计算总价
                var totalPrice = data.Sum(a => a.Price * a.Num);
                var totalNum = data.Sum(a => a.Num);

                //根据旧部门查询出是否生成取餐码,如果没有生成则生成一个，有则取.
                var takeFoodCode = (from a in Service.GetIQueryable<F_Order>()
                    join b in Service.GetIQueryable<F_UserInfo>() on a.UserInfoId equals b.Id
                    join c in Service.GetIQueryable<Base_DepartmentRelation>() on b.FullDepartment equals c.Department
                        into bc
                    from c in bc.DefaultIfEmpty()
                    where a.CreateTime >= DateTime.Now.Date && a.CreateTime < DateTime.Now.Date.AddDays(1) &&
                          c.OldDepartment == userInfo.OldDepartment
                          && a.TakeFoodCode != null
                    select a.TakeFoodCode).FirstOrDefault();
                if (string.IsNullOrEmpty(takeFoodCode))
                {
                    var maxTakeFoodCodeList = Service.GetIQueryable<F_Order>()
                        .Where(a => a.CreateTime >= DateTime.Now.Date && a.CreateTime < DateTime.Now.Date.AddDays(1))
                        .Select(a => a.TakeFoodCode).ToList();
                    if (maxTakeFoodCodeList.Count>0)
                    {
                        takeFoodCode = (maxTakeFoodCodeList.Max(a => a.ToInt()) + 1).ToString();
                        
                    }
                    else
                    {
                        takeFoodCode = "1";
                    }
                }

                //添加主表
                F_Order order = new F_Order()
                {
                    UserInfoId = userInfo.Id,
                    Id = IdHelper.GetId(),
                    OrderCode = IdHelper.GetId(),
                    Status = 1,
                    Price = totalPrice,
                    OrderCount = totalNum,
                    CreateTime = DateTime.Now,
                    TakeFoodCode = takeFoodCode,
                    TakeFoodName = userInfo.UserName,
                    CancellableTime =
                        DateTime.Now.Add(fShopInfoSet.OrderBeginEnd.Value.TimeOfDay - DateTime.Now.TimeOfDay),
                    CanEvaluableTime = DateTime.Now.AddDays(1),
                    StartReceiveTime =
                        DateTime.Now.Add(fShopInfoSet.OrderBeginEnd.Value.TimeOfDay - DateTime.Now.TimeOfDay),
                    CreatorId = userInfo.Id,
                    CreatorName = userInfo.UserName,
                    UpdateTime = DateTime.Now,
                    UpdateId = userInfo.Id,
                    UpdateName = userInfo.UserName,
                };
                Insert(order);
                //添加明细
                var orderInfoList = data.Select(a => new F_OrderInfo
                {
                    OrderCode = order.OrderCode,
                    Id = IdHelper.GetId(),
                    OrderInfoQty = a.Num,
                    PublishFoodId = a.Id,
                    CreateTime = DateTime.Now,
                    CreatorId = userInfo.Id,
                    CreatorName = userInfo.UserName,
                    UpdateTime = DateTime.Now,
                    UpdateId = userInfo.Id,
                    UpdateName = userInfo.UserName,
                }).ToList();
                Service.Insert<F_OrderInfo>(orderInfoList);

                //生成一个取餐码，然后随机一个取餐人
                var RandMan = (from a in Service.GetIQueryable<F_Order>()
                    join b in Service.GetIQueryable<F_UserInfo>() on a.UserInfoId equals b.Id
                    join c in Service.GetIQueryable<Base_DepartmentRelation>() on b.FullDepartment equals c.Department
                        into bc
                    from c in bc.DefaultIfEmpty()
                    where a.CreateTime >= DateTime.Now.Date && a.CreateTime < DateTime.Now.Date.AddDays(1) && c.OldDepartment == userInfo.OldDepartment
                    orderby Guid.NewGuid()
                    select b.UserName).FirstOrDefault();

                //更新取餐人
                var orderList = (from a in Service.GetIQueryable<F_Order>()
                    join b in Service.GetIQueryable<F_UserInfo>() on a.UserInfoId equals b.Id
                    join c in Service.GetIQueryable<Base_DepartmentRelation>() on b.FullDepartment equals c.Department
                        into bc
                    from c in bc.DefaultIfEmpty()
                    where a.CreateTime >= DateTime.Now.Date && a.CreateTime < DateTime.Now.Date.AddDays(1) &&
                          c.OldDepartment == userInfo.OldDepartment
                    select a).ToList();
                orderList.ForEach(a => { a.TakeFoodName = RandMan; });

                if(orderList.Count>0)
                 Service.UpdateAny(orderList, new List<string>() {"TakeFoodName"});
            }

            await Task.CompletedTask;
        }

        public async Task<byte[]> SumExcelToExport(ConditionDTO input)
        {
            Expression<Func<F_Order, F_UserInfo, F_PublishFood, IF_OrderResultDTO>> select = (a, b, c) =>
                new IF_OrderResultDTO
                {
                    UserName = b.UserName,
                    DepartmentName = b.FullDepartment,
                    FoodName = c.FoodName,
                    TakeFoodCode = a.TakeFoodCode,
                    TakeFoodName = a.TakeFoodName,
                    OldDepartmentName = Service.GetIQueryable<Base_DepartmentRelation>()
                        .FirstOrDefault(d => d.Department == b.FullDepartment).OldDepartment
                };

            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                join b in Service.GetIQueryable<F_UserInfo>() on a.UserInfoId equals b.Id into ab
                from b in ab.DefaultIfEmpty()
                join c in Service.GetIQueryable<F_OrderInfo>() on a.OrderCode equals c.OrderCode
                join d in Service.GetIQueryable<F_PublishFood>() on c.PublishFoodId equals d.Id
                select @select.Invoke(a, b, d);

            var where = LinqHelper.True<IF_OrderResultDTO>();
            var search = input;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {

                where = where.And(a =>
                    a.CreateTime >= input.Keyword.ToDateTime().Date && a.CreateTime <
                                                                   input.Keyword.ToDateTime().Date.AddDays(1)
                                                                   && a.Status != 4);
            }
            //增加按照部门排序
            var orderResultList = q.Where(where).OrderBy(a => a.OldDepartmentName).ToList();
            DataTable dt = orderResultList.OrderBy(a=>a.TakeFoodCode.ToInt()).GroupBy(a => new { DepartmentName=a.OldDepartmentName??a.DepartmentName, a.FoodName}).
            Select(a => new
            {
                TakeFoodCode = "A"+a.FirstOrDefault().TakeFoodCode,
                DepartmentName = a.Key.DepartmentName,
                FoodName = a.Key.FoodName,
                Count = a.Count(),
                TakeFoodName = a.FirstOrDefault().TakeFoodName,
            }
            ).ToDataTable();
            if (dt != null && dt.Rows.Count == 0) throw new BusException("无下载数据!");
            if (dt.Columns.Contains("TakeFoodCode"))
                dt.Columns["TakeFoodCode"].ColumnName = "取餐码";
            if (dt.Columns.Contains("DepartmentName"))
                dt.Columns["DepartmentName"].ColumnName = "部门名称";
            if (dt.Columns.Contains("FoodName"))
                dt.Columns["FoodName"].ColumnName = "菜品";
            if (dt.Columns.Contains("Count"))
                dt.Columns["Count"].ColumnName = "数量";
            if (dt.Columns.Contains("TakeFoodName"))
                dt.Columns["TakeFoodName"].ColumnName = "领餐人";
            await Task.CompletedTask;
            return AsposeOfficeHelper.DataTableToExcelBytes(dt);
        }

        public async Task<byte[]> ExcelToExport(ConditionDTO input)
        {
            Expression<Func<F_Order, F_UserInfo, IF_OrderResultDTO>> select = (a, b) => new IF_OrderResultDTO
            {
                UserName = b.UserName,
                DepartmentName = b.Department,
                FoodName = string.Join(",", (from c in Service.GetIQueryable<F_OrderInfo>()
                                             join d in Service.GetIQueryable<F_PublishFood>() on c.PublishFoodId equals d.Id
                                             where c.OrderCode == a.OrderCode
                                             select d.FoodName)),
                SupplierName = string.Join(",", (from c in Service.GetIQueryable<F_OrderInfo>()
                                                 join d in Service.GetIQueryable<F_PublishFood>() on c.PublishFoodId equals d.Id
                                                 where c.OrderCode == a.OrderCode
                                                 select d.SupplierName)),
                OldDepartmentName = Service.GetIQueryable<Base_DepartmentRelation>().FirstOrDefault(c => c.Department == b.FullDepartment).OldDepartment


            };

            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                    join b in Service.GetIQueryable<F_UserInfo>() on a.UserInfoId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    select @select.Invoke(a, b);

            var where = LinqHelper.True<IF_OrderResultDTO>();
            var search = input;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {

                where = where.And(a =>
                    a.CreateTime >= input.Keyword.ToDateTime().Date && a.CreateTime < input.Keyword.ToDateTime().Date.AddDays(1)
                                                              && a.Status != 4);
            }
            //增加按照部门排序
            DataTable dt = q.Where(where).OrderBy(a => a.OldDepartmentName).Select(a=>new
            {
                OldDepartmentName = a.OldDepartmentName,
                UserName =a.UserName,
                SupplierName=a.SupplierName,
                FoodName=a.FoodName,
                OrderCount=a.OrderCount,
                Price=a.Price,
                OrderCode=a.OrderCode,
                CreateTime=a.CreateTime
            }).ToList().ToDataTable();
            if (dt != null && dt.Rows.Count == 0) throw new BusException("无下载数据!");
            if (dt.Columns.Contains("UserName"))
                dt.Columns["UserName"].ColumnName = "用户名";
            if (dt.Columns.Contains("SupplierName"))
                dt.Columns["SupplierName"].ColumnName = "商户名称";
            if (dt.Columns.Contains("FoodName"))
                dt.Columns["FoodName"].ColumnName = "菜品名称";
            if (dt.Columns.Contains("DepartmentName"))
                dt.Columns["DepartmentName"].ColumnName = "部门名称";
            if (dt.Columns.Contains("OldDepartmentName"))
                dt.Columns["OldDepartmentName"].ColumnName = "部门名称";
            if (dt.Columns.Contains("OrderCount"))
                dt.Columns["OrderCount"].ColumnName = "数量";
            if (dt.Columns.Contains("Price"))
                dt.Columns["Price"].ColumnName = "价格";
            if (dt.Columns.Contains("OrderCode"))
                dt.Columns["OrderCode"].ColumnName = "订单编号";
            if (dt.Columns.Contains("CreateTime"))
                dt.Columns["CreateTime"].ColumnName = "下单时间";
            await Task.CompletedTask;
            return AsposeOfficeHelper.DataTableToExcelBytes(dt);
        }

        public async Task AddDataAsync(List<F_Order> data)
        {

            await InsertAsync(data);
        }

        public async Task<bool> CancelOrderAsync(string orderCode)
        {
            //读取订单门店是否在取消时间内
            var orderInfo= GetIQueryable().Where(a => a.OrderCode == orderCode)?.FirstOrDefault();
            if(orderInfo==null) throw new BusException("取消异常!");
            if(DateTime.Now>orderInfo.CancellableTime) throw new BusException("不再取消时间内!");
            if (orderInfo.Status == 4) throw new BusException("已经取消!");
            //增加商品数量
            var query =await Service.GetIQueryable<F_OrderInfo>().Where(a => a.OrderCode == orderCode).ToListAsync();
            query.ForEach(a =>
                {
                     Service.UpdateWhere<F_PublishFood>(b => b.Id == a.PublishFoodId,
                        b =>
                        {
                            b.FoodQty = b.FoodQty + a.OrderInfoQty;
                            b.UpdateTime = DateTime.Now;
                            b.UpdateName = oOperator.WeChatProperty.UserName;
                            b.UpdateId = oOperator.WeChatProperty.Id;

                        });
                });
            int result=  await  UpdateWhereAsync(a => a.OrderCode == orderCode, a =>
            {
                a.Status = 4;
                a.UpdateTime=DateTime.Now;
                a.UpdateName = oOperator.WeChatProperty.UserName;
                a.UpdateId = oOperator.WeChatProperty.Id;

            });
         return result > 0;
        }

        [JobDisplayName("自动刷新订单状态 每日23点执行一次")]
        public async Task RefreshOrderStatus()
        {
           //var orderInfoList=  await GetIQueryable().Where(a => a.CreateTime < DateTime.Now.Date.AddDays(1) && a.Status==1).ToListAsync();
           //orderInfoList.ForEach(a =>
           //{
           //    a.Status = 3;
           //    a.UpdateTime=DateTime.Now;
           //    a.UpdateName = "Job";
           //    a.UpdateId = "Job";
           //});
          await UpdateWhereAsync(a => a.CreateTime < DateTime.Now.Date.AddDays(1) && a.Status == 1, a =>
           {
               a.Status = 3;
               a.UpdateTime = DateTime.Now;
               a.UpdateName = "Job";
               a.UpdateId = "Job";
           });
        }

        public async Task<F_Order> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(F_Order data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(F_Order data)
        {
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        #endregion

        #region 私有成员

        #endregion
    }
}