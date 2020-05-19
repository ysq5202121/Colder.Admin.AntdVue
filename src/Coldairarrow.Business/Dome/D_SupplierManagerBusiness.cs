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
using System.Linq.Expressions;
using System;

namespace Coldairarrow.Business.Dome
{
    public class D_SupplierManagerBusiness : BaseBusiness<D_SupplierManager>, ID_SupplierManagerBusiness, ITransientDependency
    {
        public D_SupplierManagerBusiness(IRepository repository)
            : base(repository)
        {
        }

        #region 外部接口

        public async Task<PageResult<D_SupplierManagerResultDto>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var queryDic= await Service.GetIQueryable<Base_Dictionary>().ToListAsync();
            string x = queryDic.FirstOrDefault(b => b.DicKey == "Region" && b.DicValue == "1")?.DicDisplayValue;
            //var q = GetIQueryable().Select(a =>  new D_SupplierManagerResultDto 
            //{ 
            //    RegionName=queryDic.FirstOrDefault(b=>b.DicKey== "Region" && b.DicValue=="1").DicDisplayValue,
            //    Id=a.Id,
            //    SupplierName=a.SupplierName

            //});
            //var q = from a in GetIQueryable().AsExpandable()
            //        join b in Service.GetIQueryable<Base_Dictionary>() on a.Region equals  b.DicValue
            //        where b.DicKey== "Region"
            //         select new D_SupplierManagerResultDto
            //         {
            //             Id = a.Id,
            //             SupplierName = a.SupplierName,
            //             RegionName=b.DicDisplayValue

            //         };

            Expression<Func<D_SupplierManager, D_SupplierManagerResultDto>> select = a => new D_SupplierManagerResultDto
            {
        
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                    select @select.Invoke(a);
            var where = LinqHelper.True<D_SupplierManagerResultDto>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<D_SupplierManagerResultDto, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }
            var listPage = await q.Where(where).GetPageResultAsync(input);
            listPage.Data.ForEach(a => {
                a.RegionName = queryDic.FirstOrDefault(b => b.DicKey == "Region" && b.DicValue == a.Region)?.DicDisplayValue;
                a.CityName = queryDic.FirstOrDefault(b => b.DicKey == "City" && b.DicValue == a.City)?.DicDisplayValue;
                a.SupplierTypeName= queryDic.FirstOrDefault(b => b.DicKey == "SupplierType" && b.DicValue == a.SupplierType.ToString())?.DicDisplayValue;
                a.StatusName= queryDic.FirstOrDefault(b => b.DicKey == "SupplierStatus" && b.DicValue == a.STATUS.ToString())?.DicDisplayValue;
            });
            return listPage;
    }

        public async Task<SupplierManagerStatusDto> GetStatusListAsync()
        {
            var query = await Service.GetIQueryable<Base_Dictionary>().ToListAsync();
            SupplierManagerStatusDto supplierManagerStatusDto = new SupplierManagerStatusDto()
            {
                SupplierType = query.Where(a => a.DicKey == "SupplierType").Select(a => new SelectOption
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
                }).ToList(),
                Status = query.Where(a => a.DicKey == "SupplierStatus").Select(a => new SelectOption
                {
                    title = a.DicDisplayValue,
                    value = a.DicValue
                }).ToList()
            };
            return supplierManagerStatusDto;
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