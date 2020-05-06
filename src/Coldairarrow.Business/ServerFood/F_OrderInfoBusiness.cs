using System;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerFood
{
    public class F_OrderInfoBusiness : BaseBusiness<F_OrderInfo>, IF_OrderInfoBusiness, ITransientDependency
    {
        public F_OrderInfoBusiness(IRepository repository)
            : base(repository)
        {
        }

        #region 外部接口

        public async Task<PageResult<F_OrderInfo>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<F_OrderInfo>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<F_OrderInfo, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<List<IF_OrderInfoResultDto>> GetDataListNoPageAsync(ConditionDTO input)
        {
            Expression<Func<F_OrderInfo,F_PublishFood, IF_OrderInfoResultDto>> select = (a, b) => new IF_OrderInfoResultDto
            {
                FoodName = b.FoodName
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                join b in Service.GetIQueryable<F_PublishFood>() on a.PublishFoodId equals b.Id into ab
                from b in ab.DefaultIfEmpty()
                select @select.Invoke(a, b);

            var where = LinqHelper.True<IF_OrderInfoResultDto>();
            var search = input;
            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<IF_OrderInfoResultDto, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }
            return await q.Where(where).ToListAsync();
        }

        public async Task<List<IF_OrderInfoResultDto>> GetDataListToMoblieAsync(ConditionDTO input)
        {
            Expression<Func<F_OrderInfo, F_PublishFood, IF_OrderInfoResultDto>> select = (a, b) => new IF_OrderInfoResultDto
            {
                FoodName = b.FoodName,
                Price =b.Price,
                ImageUrl = b.ImgUrl,
                FoodDesc = b.FoodDesc,
                SupplierName=b.SupplierName
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                    join b in Service.GetIQueryable<F_PublishFood>() on a.PublishFoodId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    select @select.Invoke(a, b);

            var where = LinqHelper.True<IF_OrderInfoResultDto>();
            var search = input;
            where = where.And(a=>a.OrderCode==input.Keyword);

            return await q.Where(where).ToListAsync();
        }

        public async Task<F_OrderInfo> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(F_OrderInfo data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(F_OrderInfo data)
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