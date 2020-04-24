using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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

        public async Task<PageResult<F_PublishFood>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<F_PublishFood>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<F_PublishFood, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<List<F_PublishFood>> GetDataListToMoblieAsync(ConditionDTO input)
        {
            var q =  GetIQueryable();
            var where = LinqHelper.True<F_PublishFood>();
            where.And(a => a.Prcie > 0);
            //if (!input.keyword.IsNullOrEmpty())
            //{
            //    where = where.And(x => EF.Functions.Like(x.Name, $"%{input.keyword}%"));
            //}
            //if (!input.parentId.IsNullOrEmpty())
            //    where = where.And(x => x.ParentId == input.parentId);
            //if (input.types?.Count > 0)
            //    where = where.And(x => input.types.Contains((int)x.Type));

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