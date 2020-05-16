using Coldairarrow.Entity.Dome;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Coldairarrow.Entity.Base_Manage;
using Dynamitey.DynamicObjects;

namespace Coldairarrow.Business.Dome
{
    public class D_SupplierManagerBusiness : BaseBusiness<D_SupplierManager>, ID_SupplierManagerBusiness, ITransientDependency
    {
        public D_SupplierManagerBusiness(IRepository repository)
            : base(repository)
        {
        }

        #region 外部接口

        public async Task<PageResult<D_SupplierManager>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<D_SupplierManager>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<D_SupplierManager, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<SupplierManagerStatusDto> GetStatusListAsync()
        {
            var query = await Service.GetIQueryable<Base_Dictionary>().ToListAsync();
            SupplierManagerStatusDto supplierManagerStatusDto = new SupplierManagerStatusDto()
            {
                 SupplierType = query.Where(a=>a.DicKey== "SupplierType").Select(a=>new SelectOption
                 {
                    title = a.DicDisplayValue,
                    value = a.DicValue
                 }).ToList(),
                 Region = query.Where(a => a.DicKey == "Region").Select(a => new SelectOption
                 {
                     title = a.DicDisplayValue,
                     value = a.DicValue
                 }).ToList(),
                 City = query.Where(a => a.DicKey == "City").Select(a => new SelectOption
                 {
                 title = a.DicDisplayValue,
                 value = a.DicValue
            }).ToList()
            };
            return  supplierManagerStatusDto;
        }

        public async Task<D_SupplierManager> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(D_SupplierManager data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(D_SupplierManager data)
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