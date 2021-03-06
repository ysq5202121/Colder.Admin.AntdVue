﻿using System;
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
using Coldairarrow.Util.ApiHelper.WeChat;
using Hangfire;

namespace Coldairarrow.Business.ServerFood
{
    public class F_UserInfoBusiness : BaseBusiness<F_UserInfo>, IF_UserInfoBusiness, ITransientDependency
    {
        public IOperator operators;
        public F_UserInfoBusiness(IRepository repository, IOperator op)
            : base(repository)
        {
            operators = op;
        }

        #region 外部接口

        public async Task<PageResult<IF_UserInfoResultDto>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            Expression<Func<F_UserInfo, F_ShopInfo, IF_UserInfoResultDto>> select = (a, b) => new IF_UserInfoResultDto
            {
                ShopName = b.ShopName
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                join b in Service.GetIQueryable<F_ShopInfo>() on a.ShopInfoId equals b.Id into ab
                from b in ab.DefaultIfEmpty()
                select @select.Invoke(a, b);

            var where = LinqHelper.True<IF_UserInfoResultDto>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<IF_UserInfoResultDto, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<IF_UserInfoResultDto> GetUserInfoToMoblieAsync()
        {
            var q = from a in GetIQueryable().AsExpandable()
                join b in Service.GetIQueryable<F_ShopInfo>() on a.ShopInfoId equals b.Id into ab
                from b in ab.DefaultIfEmpty()
                where a.WeCharUserId == operators.UserId
                select new IF_UserInfoResultDto
                {
                    ShopName = b.ShopName,
                    UserName = a.UserName,
                    UserImgUrl = a.UserImgUrl,
                    Department = a.Department

                };
            return await q.FirstOrDefaultAsync();
        }

        public async Task<F_UserInfo> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }
        public async Task<F_UserInfo> GetTheDataByWeChatIdAsync(string id)
        {
            return await GetIQueryable().Where(a => a.WeCharUserId == id).FirstOrDefaultAsync();
        }

        public async Task AddDataAsync(F_UserInfo data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(F_UserInfo data)
        {
            await UpdateAsync(data);
        }

        public async Task UpdateShopNameAsync(F_UserInfo data)
        {
            var userInfo = Service.GetIQueryable<F_UserInfo>().Where(a => a.WeCharUserId == operators.UserId)?.FirstOrDefault();
            if (userInfo == null) throw new BusException("获取用户信息失败!");
            userInfo.ShopInfoId = data.ShopInfoId;
            userInfo.UpdateId = userInfo.Id;
            userInfo.UpdateName = userInfo.UserName;
            userInfo.UpdateTime = userInfo.UpdateTime;
            await Service.UpdateAnyAsync(userInfo, new List<string> { "ShopInfoId", "UpdateId" , "UpdateName", "UpdateTime" });
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        public async Task<string> Login(string code)
        {
            string token = WeChatOperation.GetToken(EnumWeChatAppType.Food);
            if (token == null) throw new BusException("获取授权token失败!");
            string userId = WeChatOperation.GetUserId(code, token);
            if (userId == null) throw new BusException("获取授权userId失败");
            //缓存token,不重复获取
            //查询用户信息
            var userInfo = GetIQueryable().Where(a => a.WeCharUserId == userId)?.FirstOrDefault();
            if (userInfo == null || string.IsNullOrEmpty(userInfo.Department))
            {
                WeChatUserInfo weChatUserInfo = WeChatOperation.GetUserInfo(token, userId);
                WeChatDepartmentList weChatDepartmentList = null;
                string departmentPath = string.Empty;
                string departmentId = string.Empty;
                string department = string.Empty;
                if (weChatUserInfo == null) throw new BusException("获取用户信息失败,登录失败!");
                if (!string.IsNullOrEmpty(weChatUserInfo.main_department))
                {
                    //底层部门
                    weChatDepartmentList = WeChatOperation.GetDepartment(token, weChatUserInfo.main_department);
                    if (weChatDepartmentList != null)
                    {
                        departmentId = weChatDepartmentList?.department?.FirstOrDefault()?.parentid;
                        departmentPath = weChatDepartmentList?.department?.FirstOrDefault()?.name;
                        department = weChatDepartmentList?.department?.FirstOrDefault()?.name;
                    }
                    //倒数第二
                    weChatDepartmentList = WeChatOperation.GetDepartment(token, departmentId);
                    if (weChatDepartmentList != null)
                    {   
                       
                        departmentPath =
                            weChatDepartmentList?.department?.Where(a => a.id == departmentId)?.FirstOrDefault()?.name +
                            "/" + departmentPath;
                        departmentId = weChatDepartmentList?.department?.Where(a => a.id == departmentId)
                            ?.FirstOrDefault()
                            ?.parentid;
                    }
                    //倒数第三
                    weChatDepartmentList = WeChatOperation.GetDepartment(token, departmentId);
                    if (weChatDepartmentList != null)
                    {
                      
                        departmentPath =
                            weChatDepartmentList?.department?.Where(a => a.id == departmentId)?.FirstOrDefault()?.name +
                            "/" + departmentPath;
                    }
                }
                if (userInfo == null)
                {
                    F_UserInfo fUserInfo = new F_UserInfo()
                    {
                        Id = IdHelper.GetId(),
                        IsAdmin = false,
                        ShopInfoId = "",
                        UserName = weChatUserInfo.name,
                        UserImgUrl = weChatUserInfo.avatar,
                        WeCharUserId = weChatUserInfo.userid,
                        Department = department,
                        FullDepartment = departmentPath,
                        CreateTime = DateTime.Now,
                        CreatorId = "system",
                        CreatorName = weChatUserInfo.name,
                    };
                    await InsertAsync(fUserInfo);
                }
                else if (string.IsNullOrEmpty(userInfo.Department) || string.IsNullOrEmpty(userInfo.FullDepartment))
                {
                    //更新部门//组合倒数第二级部门
                    userInfo.Department = department;
                    userInfo.FullDepartment = departmentPath;
                    userInfo.UpdateTime = DateTime.Now;
                    userInfo.UpdateName = userInfo.UserName;
                    userInfo.UpdateId = userInfo.UpdateId;
                    await Service.UpdateAnyAsync(userInfo,
                        new List<string>() {"Department", "UpdateTime", "UpdateName", "UpdateId"});
                }
            }

            //生成token,有效期一个月
            JWTPayload jWTPayload = new JWTPayload
            {
                UserId = userId,
                Expire = DateTime.Now.AddDays(30),
                AppId = (int)EnumWeChatAppType.Food
            };
            string tk = JWTHelper.GetToken(jWTPayload.ToJson(), JWTHelper.JWTClient);
            return tk;
        }

        public async Task<bool> CheckLogin(string code)
        {
            string token = WeChatOperation.GetToken(EnumWeChatAppType.Food);
            if (token == null) throw new BusException("获取授权token失败!");
            string userId = WeChatOperation.GetUserId(code, token);
            if (userId == null) throw new BusException("获取授权userId失败");
            var userInfo = await GetIQueryable().Where(a => a.WeCharUserId == userId)?.FirstOrDefaultAsync();
            return userInfo==null;
        }

        [JobDisplayName("自动更新部门 每周日执行一次")]
        public async Task TimedRefreshDepartment()
        {
            string token = WeChatOperation.GetToken(EnumWeChatAppType.Food);
            if (token == null) throw new BusException("获取授权token失败!");
            var query = GetList();
            foreach (var item in query)
            {
                WeChatUserInfo weChatUserInfo = WeChatOperation.GetUserInfo(token, item.WeCharUserId);
                WeChatDepartmentList weChatDepartmentList = null;
                string departmentPath = string.Empty;
                string departmentId = string.Empty;
                string department = string.Empty;
                if (weChatUserInfo == null) continue;
                //底层部门
                weChatDepartmentList = WeChatOperation.GetDepartment(token, weChatUserInfo.main_department);
                if (weChatDepartmentList == null) continue;
                departmentId= weChatDepartmentList?.department?.FirstOrDefault()?.parentid;
                departmentPath = weChatDepartmentList?.department?.FirstOrDefault()?.name;
                department = weChatDepartmentList?.department?.FirstOrDefault()?.name;

                //倒数第二
                weChatDepartmentList = WeChatOperation.GetDepartment(token, departmentId);
                if (weChatDepartmentList == null) continue;
                departmentPath = weChatDepartmentList?.department?.Where(a => a.id == departmentId)?.FirstOrDefault()?.name + "/" + departmentPath;
                departmentId = weChatDepartmentList?.department?.Where(a => a.id == departmentId)?.FirstOrDefault()
                    ?.parentid;
                //倒数第三
                weChatDepartmentList = WeChatOperation.GetDepartment(token, departmentId);
                if (weChatDepartmentList == null) continue;
                departmentPath = weChatDepartmentList?.department?.Where(a => a.id == departmentId)?.FirstOrDefault()?.name + "/" + departmentPath;

                item.FullDepartment = departmentPath;
                item.Department = department;
                item.UpdateTime = DateTime.Now;
                item.UpdateName = "Job";
                item.UpdateId = "Job";
                await Service.UpdateAnyAsync(item,
                    new List<string>() { "Department", "FullDepartment", "UpdateTime", "UpdateName", "UpdateId"});
            }
        }

        #endregion

        #region 私有成员

        #endregion
    }
}