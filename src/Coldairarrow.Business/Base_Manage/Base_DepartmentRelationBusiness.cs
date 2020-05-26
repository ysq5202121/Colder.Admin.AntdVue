using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Coldairarrow.Entity.ServerFood;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_DepartmentRelationBusiness : BaseBusiness<Base_DepartmentRelation>, IBase_DepartmentRelationBusiness, ITransientDependency
    {
        public Base_DepartmentRelationBusiness(IRepository repository)
            : base(repository)
        {
        }

        #region 外部接口

        public async Task<PageResult<Base_DepartmentRelation>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<Base_DepartmentRelation>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Base_DepartmentRelation, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }
        public async Task<List<string>> GetNewDepartmentListAsync()
        {
            var query = from a in Service.GetIQueryable<F_UserInfo>()
                where !GetIQueryable().Select(b => b.Department).Contains(a.Department)
                select a.Department;

            return await query.Distinct().ToListAsync();
        }
        public async Task<Base_DepartmentRelation> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Base_DepartmentRelation data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Base_DepartmentRelation data)
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