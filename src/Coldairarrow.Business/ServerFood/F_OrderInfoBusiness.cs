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
using Quartz.Impl.Matchers;

namespace Coldairarrow.Business.ServerFood
{
    public class F_OrderInfoBusiness : BaseBusiness<F_OrderInfo>, IF_OrderInfoBusiness, ITransientDependency
    {
        public IOperator oOperator;
        public F_OrderInfoBusiness(IRepository repository, IOperator op)
            : base(repository)
        {
            oOperator = op;
        }

        #region 外部接口

        public async Task<PageResult<F_OrderInfo>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<F_OrderInfo>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<F_OrderInfo, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<List<IF_OrderInfoResultDto>> GetDataListNoPageAsync(ConditionDTO input)
        {
            Expression<Func<F_OrderInfo,F_PublishFood, IF_OrderInfoResultDto>> select = (a, b) => new IF_OrderInfoResultDto
            {
                FoodName = b.FoodName
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                join b in Service.GetIQueryable<F_PublishFood>() on a.PublishFoodId equals b.Id into ab
                from b in ab.DefaultIfEmpty()
                select @select.Invoke(a, b);

            var where = LinqHelper.True<IF_OrderInfoResultDto>();
            var search = input;
            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<IF_OrderInfoResultDto, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }
            return await q.Where(where).ToListAsync();
        }

        public async Task<List<IF_OrderInfoResultDto>> GetDataListToMoblieAsync(ConditionDTO input)
        {
            Expression<Func<F_OrderInfo, F_PublishFood, IF_OrderInfoResultDto>> select = (a, b) => new IF_OrderInfoResultDto
            {
                FoodName = b.FoodName,
                Price =b.Price,
                ImageUrl = b.ImgUrl,
                FoodDesc = b.FoodDesc,
                SupplierName=b.SupplierName
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                    join b in Service.GetIQueryable<F_PublishFood>() on a.PublishFoodId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    select @select.Invoke(a, b);

            var where = LinqHelper.True<IF_OrderInfoResultDto>();
            var search = input;
            where = where.And(a=>a.OrderCode==input.Keyword);

            return await q.Where(where).ToListAsync();
        }

        /// <summary>
        /// 扫描取同一个部门餐
        /// </summary>
        /// <returns></returns>
        public async Task<List<IF_OrderInfoResultDto>> ScanCodeAsyns(DateTime? day)
        {
            var userInfo = Service.GetIQueryable<F_UserInfo>().Where(a => a.WeCharUserId == oOperator.UserId)?.FirstOrDefault();
            if (userInfo == null) throw new BusException("获取用户信息失败!");
            if(!day.HasValue)day=DateTime.Now;
            Expression<Func<F_OrderInfo, F_PublishFood, Base_DepartmentRelation, IF_OrderInfoResultDto>> select = (a, b,c) => new IF_OrderInfoResultDto
            {
                FoodName = b.FoodName,
                Price = b.Price,
                ImageUrl = b.ImgUrl,
                FoodDesc = b.FoodDesc,
                SupplierName = b.SupplierName,
                OldDepartment = c.OldDepartment,
                order = a.CreatorId== userInfo.Id ? 1:0
            };
            var toDay = DateTime.Now.Date;
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                join b in Service.GetIQueryable<F_PublishFood>() on a.PublishFoodId equals b.Id 
                join e in Service.GetIQueryable<F_Order>() on a.OrderCode equals e.OrderCode
                join c in Service.GetIQueryable<F_UserInfo>() on a.CreatorId equals c.Id
                join d in Service.GetIQueryable<Base_DepartmentRelation>() on c.FullDepartment equals d.Department into cd
                from d in cd.DefaultIfEmpty()
                where a.CreateTime >= day.Value.Date && a.CreateTime < day.Value.Date.AddDays(1) && e.Status!=4
                select @select.Invoke(a, b,d);
                
            var where = LinqHelper.True<IF_OrderInfoResultDto>();

            //读取旧部门
            var oldDepartment = Service.GetIQueryable<Base_DepartmentRelation>()
                .FirstOrDefault(a => a.Department == userInfo.FullDepartment)?.OldDepartment;
            
            if (!oldDepartment.IsNullOrEmpty())
            {
                where = where.And(a =>
                    a.OldDepartment == oldDepartment);
            }
            else
            {
                where = where.And(a =>
                    a.CreatorId == userInfo.Id);
            }

            return await q.Where(where).OrderByDescending(a=>a.order).ToListAsync();

        }

        public async Task<F_OrderInfo> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(F_OrderInfo data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(F_OrderInfo data)
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