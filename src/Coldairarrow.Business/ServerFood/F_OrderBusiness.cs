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

namespace Coldairarrow.Business.ServerFood
{
    public class F_OrderBusiness : BaseBusiness<F_Order>, IF_OrderBusiness, ITransientDependency
    {
        public IOperator oOperator;
        public F_OrderBusiness(IRepository repository, IOperator op)
            : base(repository)
        {
            oOperator = op;
        }

        #region 外部接口

        public async Task<PageResult<IF_OrderResultDTO>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            Expression<Func<F_Order, F_UserInfo, IF_OrderResultDTO >> select = (a, b) => new IF_OrderResultDTO
            {
                UserName = b.UserName
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                join b in Service.GetIQueryable<F_UserInfo>() on a.UserInfoId equals b.Id into ab
                from b in ab.DefaultIfEmpty()
                select @select.Invoke(a, b);
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
            if (data == null || data.Count==0)
            {
                throw new BusException("数据不正常请检查!");
            }
            var userInfo = Service.GetIQueryable<F_UserInfo>().Where(a => a.WeCharUserId == oOperator.UserId)?.FirstOrDefault();
            if(userInfo==null) throw new BusException("获取用户信息失败!");
            if(string.IsNullOrEmpty(userInfo.ShopInfoId)) throw new BusException("请先到[我的]绑定门店!");
            //查询发布菜品信息
            var fPublishFoodList = Service.GetIQueryable<F_PublishFood>().Where(a => data.Select(b => b.Id).Contains(a.Id)).ToList();
            if(fPublishFoodList.Count==0) throw new BusException("数据查询错误!");
            var fShopInfoSet= Service.GetIQueryable<F_ShopInfoSet>().Where(a => a.ShopInfoId == fPublishFoodList.FirstOrDefault().ShopInfoId).FirstOrDefault();
            if(fShopInfoSet==null) throw new BusException("门店设置异常!");
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
                        throw new BusException("商品["+a.FoodName+"]限购"+a.Limit+"件");
                    }
                }
            });
            //检查订单SKU数量
            if (fShopInfoSet.OrderFoodTypeNum.HasValue)
            {
                if (data.Count > fShopInfoSet.OrderFoodTypeNum)
                {
                    throw new BusException("订单只能选择"+ fShopInfoSet.OrderFoodTypeNum+"种商品");
                }
            }//检查当天订单数量
            if (fShopInfoSet.UserOrderNum.HasValue)
            {
                var toDay = DateTime.Now.Date; 
                var taDayOrderNum= Service.GetIQueryable<F_Order>().Count(a=>a.UserInfoId==userInfo.Id &&  a.CreateTime > toDay && a.CreateTime < toDay.AddDays(1) && a.Status!=4);
                if(taDayOrderNum >= fShopInfoSet.UserOrderNum) throw new BusException("今天已下单"+ taDayOrderNum+"个,不能再下单");
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
            await Service.UpdateAnyAsync<F_PublishFood>(fPublishFoodList,new List<string>(){ "FoodQty" });
            //计算总价
            var totalPrice = data.Sum(a => a.Price * a.Num);
            var totalNum = data.Sum(a => a.Num);
            //添加主表
            F_Order order= new F_Order()
            {
                UserInfoId = userInfo.Id,
                Id= IdHelper.GetId(),
                OrderCode = IdHelper.GetId(),
                Status = 1,
                Price = totalPrice,
                OrderCount = totalNum,
                CreateTime = DateTime.Now,
                CancellableTime = DateTime.Now.Add(fShopInfoSet.OrderBeginEnd.Value.TimeOfDay-DateTime.Now.TimeOfDay),
                CanEvaluableTime = DateTime.Now.AddDays(1),
                StartReceiveTime = DateTime.Now.Add(fShopInfoSet.OrderBeginEnd.Value.TimeOfDay - DateTime.Now.TimeOfDay),
                CreatorId = userInfo.Id,
                CreatorName= userInfo.UserName,
                UpdateTime = DateTime.Now,
                UpdateId = userInfo.Id,
                UpdateName = userInfo.UserName,
            };
           await InsertAsync(order);
            //添加明细
           var orderInfoList=data.Select(a => new F_OrderInfo 
            {
               OrderCode= order.OrderCode,
               Id= IdHelper.GetId(),
               OrderInfoQty=a.Num,
               PublishFoodId=a.Id,
               CreateTime = DateTime.Now,
               CreatorId = userInfo.Id,
               CreatorName = userInfo.UserName,
               UpdateTime = DateTime.Now,
               UpdateId = userInfo.Id,
               UpdateName = userInfo.UserName,
           }).ToList();
           await  Service.InsertAsync<F_OrderInfo>(orderInfoList);
           await Task.CompletedTask;
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
                OldDepartmentName = Service.GetIQueryable<Base_DepartmentRelation>().FirstOrDefault(c =>c.Department==b.Department).OldDepartment


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
                    a.CreateTime > input.Keyword.ToDateTime() && a.CreateTime < input.Keyword.ToDateTime().AddDays(1)
                                                              && a.Status != 4);
            }

            DataTable dt = q.Where(where).ToList().ToDataTable();
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
                dt.Columns["OldDepartmentName"].ColumnName = "旧部门";
            if (dt.Columns.Contains("OrderCount"))
                dt.Columns["OrderCount"].ColumnName = "数量";
            if (dt.Columns.Contains("Price"))
                dt.Columns["Price"].ColumnName = "价格";
            if (dt.Columns.Contains("OrderCode"))
                dt.Columns["OrderCode"].ColumnName = "订单编号";
            if (dt.Columns.Contains("CreateTime"))
                dt.Columns["CreateTime"].ColumnName = "下单时间";
            if (dt.Columns.Contains("CreatorId"))
                dt.Columns.Remove(dt.Columns["CreatorId"]);
            if (dt.Columns.Contains("Id"))
                dt.Columns.Remove(dt.Columns["Id"]);
            if (dt.Columns.Contains("UserInfoId"))
                dt.Columns.Remove(dt.Columns["UserInfoId"]);
            if (dt.Columns.Contains("CreatorName"))
                dt.Columns.Remove(dt.Columns["CreatorName"]);
            if (dt.Columns.Contains("UpdateId"))
                dt.Columns.Remove(dt.Columns["UpdateId"]);
            if (dt.Columns.Contains("UpdateName"))
                dt.Columns.Remove(dt.Columns["UpdateName"]);
            if (dt.Columns.Contains("UpdateTime"))
                dt.Columns.Remove(dt.Columns["UpdateTime"]);
            if (dt.Columns.Contains("ImageUrl"))
                dt.Columns.Remove(dt.Columns["ImageUrl"]);
            if (dt.Columns.Contains("Status"))
                dt.Columns.Remove(dt.Columns["Status"]);
            if (dt.Columns.Contains("StatusName"))
                dt.Columns.Remove(dt.Columns["StatusName"]);
            if (dt.Columns.Contains("CancellableTime"))
                dt.Columns.Remove(dt.Columns["CancellableTime"]);
            if (dt.Columns.Contains("CanEvaluableTime"))
                dt.Columns.Remove(dt.Columns["CanEvaluableTime"]);
            if (dt.Columns.Contains("StartReceiveTime"))
                dt.Columns.Remove(dt.Columns["StartReceiveTime"]);
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
            int result=  await  UpdateWhereAsync(a => a.OrderCode == orderCode, a =>
            {
                a.Status = 4;
                a.UpdateTime=DateTime.Now;
                a.UpdateName = oOperator.WeChatProperty.UserName;
                a.UpdateId = oOperator.WeChatProperty.Id;

            });
         return result > 0;
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