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
    public class F_PublishFoodBusiness : BaseBusiness<F_PublishFood>, IF_PublishFoodBusiness, ITransientDependency
    {
        public F_PublishFoodBusiness(IRepository repository)
            : base(repository)
        {
        }

        #region 外部接口

        public async Task<PageResult<F_PublishFoodResultDto>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            Expression<Func<F_PublishFood, F_ShopInfo, F_PublishFoodResultDto>> select = (a, b) => new F_PublishFoodResultDto
            {
                ShopName = b.ShopName
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                    join b in Service.GetIQueryable<F_ShopInfo>() on a.ShopInfoId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    select @select.Invoke(a, b);

            var where = LinqHelper.True<F_PublishFoodResultDto>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<F_PublishFoodResultDto, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<List<F_PublishFoodResultDto>> GetDataListToMoblieAsync(ConditionDTO input)
        {
            Expression<Func<F_PublishFood, F_FoodInfo,F_ShopInfoSet, F_PublishFoodResultDto>> select = (a, b,c) => new F_PublishFoodResultDto
            {
                Sorce = b.Score,
                BeginTime=c.OrderBeginDate,
                EndTime=c.OrderBeginEnd
                
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                join b in Service.GetIQueryable<F_FoodInfo>() on a.ShopInfoId equals b.Id into ab
                from b in ab.DefaultIfEmpty()
                join c in Service.GetIQueryable<F_ShopInfoSet>() on a.ShopInfoId equals c.ShopInfoId into ac
                from c in ac.DefaultIfEmpty()
                select @select.Invoke(a, b,c);

            var where = LinqHelper.True<F_PublishFoodResultDto>();
            var toDay = DateTime.Now.Date;
            where=where.And(a => a.PublishDate > toDay && a.PublishDate < toDay.AddDays(1));
            return await q.Where(where).OrderBy(x => x.Id).ToListAsync();
        }

        public async Task<F_PublishFood> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(F_PublishFood data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(F_PublishFood data)
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