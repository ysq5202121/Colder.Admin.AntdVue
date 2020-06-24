using System;
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
        readonly IOperator _operator;
        public Base_DepartmentRelationBusiness(IRepository repository ,IOperator @operator)
            : base(repository)
        {
            _operator = @operator;
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
                where !GetIQueryable().Select(b => b.Department).Contains(a.FullDepartment)
                select a.FullDepartment;

            return await query.Where(a=>a!=null).Distinct().ToListAsync();
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

        public async Task AddDataListAsync(DepartmentRelationInputDto data)
        {
            data.Department.ForEach(a =>
            {
                Base_DepartmentRelation base_DepartmentRelation = new Base_DepartmentRelation();
                base_DepartmentRelation.Id = IdHelper.GetId();
                base_DepartmentRelation.OldDepartment = data.OldDepartment;
                base_DepartmentRelation.Department = a;
                base_DepartmentRelation.CreateTime=DateTime.Now;
                base_DepartmentRelation.CreatorName = _operator.Property.UserName;
                base_DepartmentRelation.CreatorId = _operator.UserId;
                Insert(base_DepartmentRelation);
            });
            await Task.CompletedTask;
        }

        public async Task UpdateDataListAsync(DepartmentRelationInputDto data)
        {
          
            data.Department.ForEach(a =>
            {
                Base_DepartmentRelation base_DepartmentRelation = new Base_DepartmentRelation();
                base_DepartmentRelation.Id = data.Id;
                base_DepartmentRelation.OldDepartment = data.OldDepartment;
                base_DepartmentRelation.Department = a;
                base_DepartmentRelation.UpdateTime = DateTime.Now;
                base_DepartmentRelation.UpdateName = _operator.Property.UserName;
                base_DepartmentRelation.UpdateId = _operator.UserId;

                Service.UpdateAny<Base_DepartmentRelation>(base_DepartmentRelation, new List<string>() { "OldDepartment", "Department", "UpdateTime", "UpdateName", "UpdateId" });
            });
            await Task.CompletedTask;
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