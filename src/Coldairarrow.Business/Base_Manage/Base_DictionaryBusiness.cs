using System;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Coldairarrow.Business.ServerFood;
using Coldairarrow.Entity.ServerFood;

namespace Coldairarrow.Business.Base_Manage
{
    public class Base_DictionaryBusiness : BaseBusiness<Base_Dictionary>, IBase_DictionaryBusiness, ITransientDependency
    {
        public Base_DictionaryBusiness(IRepository repository)
            : base(repository)
        {
        }

        #region 外部接口

        public async Task<PageResult<Base_DictionaryResultDto>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
       
            var q = GetIQueryable().Select(a=>new Base_DictionaryResultDto
            {
                Id=a.Id,
                DicDesc = a.DicDesc,
                DicDisplayValue = a.DicDisplayValue,
                DicKey = a.DicKey,
                DicValue = a.DicValue,
                STATUS = a.STATUS,
                CreateDate = a.CreateDate,
                CreatorId = a.CreatorId,
                CreatorName = a.CreatorName,
                UpdateId = a.UpdateId,
                UpdateTime = a.UpdateTime,
                UpdateName = a.UpdateName
            });
            var where = LinqHelper.True<Base_DictionaryResultDto>();
            var search = input.Search;
            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<Base_DictionaryResultDto, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<Base_Dictionary> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(Base_Dictionary data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(Base_Dictionary data)
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