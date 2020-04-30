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
using System.Threading.Tasks;

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

        public async Task<PageResult<F_Order>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<F_Order>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<F_Order, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task PlaceOrderAsync(List<IF_OrderInputDTO> data)
        {
            if (data == null || data.Count==0)
            {
                throw new BusException("数据不正常请检查!");
            }
            var userInfo = Service.GetIQueryable<F_UserInfo>().Where(a => a.WeCharUserId == oOperator.UserId)?.FirstOrDefault();
            if(userInfo==null) throw new BusException("获取用户信息失败!");
            //查询发布菜品信息
            var fPublishFoodList = Service.GetIQueryable<F_PublishFood>().Where(a => data.Select(b => b.Id).Contains(a.Id)).ToList();
            if (fPublishFoodList.Any(a => a.FoodQty <= 0))
            {
                throw new BusException("商品已经售罄请重新选择!");
            }
            //查询门店是否可以点菜，是否在点餐时间内
            fPublishFoodList.ForEach(a =>
            {
                a.FoodQty = a.FoodQty - data.First(b => b.Id == a.Id).Num;
            });
            await Service.UpdateAsync<F_PublishFood>(fPublishFoodList);
            //计算总价
            var totalPrice = data.Sum(a => a.Price * a.Num);
            var totalNum = data.Sum(a => a.Num);
            //添加主表
            F_Order order= new F_Order()
            {
                UserInfoId = userInfo.Id,
                Id= IdHelper.GetId(),
                OrderCode = IdHelper.GetId(),
                Price = totalPrice,
                OrderCount = totalNum,
                CreateTime = DateTime.Now,
                CreatorId= userInfo.Id,
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

        public async Task ExcelToExport()
        {

            await Task.CompletedTask;
        }
        public async Task AddDataAsync(List<F_Order> data)
        {

            await InsertAsync(data);
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