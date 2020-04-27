using System;
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
    public class F_ShopInfoBusiness : BaseBusiness<F_ShopInfo>, IF_ShopInfoBusiness, ITransientDependency
    {
        public IOperator operate;
        public F_ShopInfoBusiness(IRepository repository,IOperator op)
            : base(repository)
        {
            operate = op;
        }

        #region 外部接口

        public async Task<PageResult<F_ShopInfo>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<F_ShopInfo>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<F_ShopInfo, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<F_ShopInfo> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task<List<F_ShopInfo>> GetDataListAllAsync()
        {
            return await GetListAsync();
        }

        public async Task AddDataAsync(F_ShopInfo data)
        {
            F_ShopInfoSet shopInfoSet = new F_ShopInfoSet()
            {
                Id = IdHelper.GetId(),
                CreatorId = operate.UserId,
                CreatorName = operate.Property.RealName,
                ShopInfoId = data.Id,
                CreateTime = DateTime.Now
            };
            await Service.InsertAsync(shopInfoSet);
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(F_ShopInfo data)
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