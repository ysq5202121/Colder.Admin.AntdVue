using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerFood
{
    public class F_OrderBusiness : BaseBusiness<F_Order>, IF_OrderBusiness, ITransientDependency
    {
        public F_OrderBusiness(IRepository repository)
            : base(repository)
        {
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
            //查询发布菜品信息
            var f_PublishFoodList = Service.GetIQueryable<F_PublishFood>().Where(a => data.Select(b => b.Id).Contains(a.Id)).ToList();
            f_PublishFoodList.ForEach(a =>
            {
                a.FoodQty = a.FoodQty - data.First(b => b.Id == a.Id).Num;
                Service.Update(a);
            });
            //计算总价
            var TotalPrice = data.Sum(a => a.Price * a.Num);
            var TotalNum = data.Sum(a => a.Num);
            //添加主表
            F_Order order= new F_Order()
            {
                Id= IdHelper.GetId(),
                OrderCode = IdHelper.GetId(),
                Price = TotalPrice,
                OrderCount = TotalNum,
                CreateTime = DateTime.Now,
                CreatorId="",
                CreatorName="",
                UpdateTime = DateTime.Now,
                UpdateId="",
                UpdateName=""
            };
           await InsertAsync(order);
            //添加明细
           var OrderInfoList=data.Select(a => new F_OrderInfo 
            {
               OrderCode= order.OrderCode,
               Id= IdHelper.GetId(),
               OrderInfoQty=a.Num,
               PublishFoodId=a.Id,
               CreateTime = DateTime.Now,
               CreatorId = "",
               CreatorName = "",
               UpdateTime = DateTime.Now,
               UpdateId = "",
               UpdateName = ""
           }).ToList();
           Service.BulkInsert<F_OrderInfo>(OrderInfoList);

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