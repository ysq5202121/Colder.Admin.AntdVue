using System;
using Coldairarrow.Entity.ServerReport;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util.ApiHelper.WeChat;

namespace Coldairarrow.Business.ServerReport
{
    public class RP_UserInfoBusiness : BaseBusiness<RP_UserInfo>, IRP_UserInfoBusiness, ITransientDependency
    {
        public RP_UserInfoBusiness(IRepository repository)
            : base(repository)
        {
        }

        #region 外部接口

        public async Task<PageResult<RP_UserInfo>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<RP_UserInfo>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<RP_UserInfo, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }


        public async Task<string> Login(string code)
        {
            string token = WeChatOperation.GetToken(EnumWeChatAppType.Report);
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
                    RP_UserInfo fUserInfo = new RP_UserInfo()
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
                        new List<string>() { "Department", "UpdateTime", "UpdateName", "UpdateId" });
                }
            }
            //生成token,有效期一个月
            JWTPayload jWTPayload = new JWTPayload
            {
                UserId = userId,
                Expire = DateTime.Now.AddDays(30),
                AppId = (int)EnumWeChatAppType.Report
            };
            string tk = JWTHelper.GetToken(jWTPayload.ToJson(), JWTHelper.JWTClient);
            return tk;
        }

        public async Task<RP_UserInfo> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(RP_UserInfo data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(RP_UserInfo data)
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