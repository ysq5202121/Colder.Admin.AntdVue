using Coldairarrow.Entity.Dome;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Dome
{
    public class D_SupplierContactsBusiness : BaseBusiness<D_SupplierContacts>, ID_SupplierContactsBusiness, ITransientDependency
    {
        public D_SupplierContactsBusiness(IRepository repository)
            : base(repository)
        {
        }

        #region 外部接口

        public async Task<PageResult<D_SupplierContacts>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<D_SupplierContacts>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<D_SupplierContacts, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }
        public async Task<List<D_SupplierContacts>> GetDataListByIdAsync(ConditionDTO input)
        {
            var q = GetIQueryable();

            var where = LinqHelper.True<D_SupplierContacts>();
            var search = input;
            where = where.And(a => a.SupplierId == input.Keyword);

            return await q.Where(where).ToListAsync();
        }

        public async Task<D_SupplierContacts> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(D_SupplierContacts data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(D_SupplierContacts data)
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