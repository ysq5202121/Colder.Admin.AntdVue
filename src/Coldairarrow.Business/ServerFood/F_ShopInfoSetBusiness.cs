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
using Coldairarrow.Entity.Base_Manage;

namespace Coldairarrow.Business.ServerFood
{
    public class F_ShopInfoSetBusiness : BaseBusiness<F_ShopInfoSet>, IF_ShopInfoSetBusiness, ITransientDependency
    {
        public F_ShopInfoSetBusiness(IRepository repository)
            : base(repository)
        {
        }

        #region 外部接口

        public async Task<PageResult<IF_ShopInfoSetResultDTO>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            Expression<Func<F_ShopInfoSet, F_ShopInfo, IF_ShopInfoSetResultDTO>> select = (a, b) => new IF_ShopInfoSetResultDTO
            {
                ShopName = b.ShopName
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                join b in Service.GetIQueryable<F_ShopInfo>() on a.ShopInfoId equals b.Id into ab
                from b in ab.DefaultIfEmpty()
                select @select.Invoke(a, b);


            var where = LinqHelper.True<IF_ShopInfoSetResultDTO>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<IF_ShopInfoSetResultDTO, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }


            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<F_ShopInfoSet> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(F_ShopInfoSet data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(F_ShopInfoSet data)
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