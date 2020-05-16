using System;
using Coldairarrow.Entity.ServerRoom;
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

namespace Coldairarrow.Business.ServerRoom
{
    public class C_ConferenceRoomBusiness : BaseBusiness<C_ConferenceRoom>, IC_ConferenceRoomBusiness, ITransientDependency
    {
        public C_ConferenceRoomBusiness(IRepository repository)
            : base(repository)
        {
        }

        #region 外部接口

        public async Task<PageResult<C_ConferenceRoomResultDto>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            Expression<Func<C_ConferenceRoom, C_Office, C_ConferenceRoomResultDto>> select = (a, b) => new C_ConferenceRoomResultDto
            {
                OfficeName =  b.OfficeName
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                join b in Service.GetIQueryable<C_Office>() on a.OfficeId equals b.Id into ab
                from b in ab.DefaultIfEmpty()
                select @select.Invoke(a, b);

            var where = LinqHelper.True<C_ConferenceRoomResultDto>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<C_ConferenceRoomResultDto, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<C_ConferenceRoom> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(C_ConferenceRoom data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(C_ConferenceRoom data)
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