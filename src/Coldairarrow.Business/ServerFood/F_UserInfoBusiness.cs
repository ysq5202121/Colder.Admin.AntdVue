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
using Coldairarrow.Util.ApiHelper.WeChat;

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

        public async Task<PageResult<F_UserInfo>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<F_UserInfo>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<F_UserInfo, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<F_UserInfo> GetUserInfoToMoblieAsync()
        {
            return await GetIQueryable().Where(a => a.WeCharUserId == operators.UserId).FirstOrDefaultAsync();
        }

        public async Task<F_UserInfo> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(F_UserInfo data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(F_UserInfo data)
        {
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        public async Task<string> Login(string code)
        {
           string token= WeChatOperation.GetToken();
           if(token==null) throw new BusException("获取授权失败!");
           string userId = WeChatOperation.GetUserId(code,token);
           if (userId == null) throw new BusException("授权登录失败!");
           //缓存token,不重复获取

           //查询用户信息
           var userInfo = GetIQueryable().Where(a => a.WeCharUserId == userId)?.FirstOrDefault();
           if(userInfo==null)
           {
              WeChatUserInfo weChatUserInfo= WeChatOperation.GetUserInfo(token, userId);
              if(weChatUserInfo==null) throw new BusException("获取用户信息失败,登录失败!");
              WeChatDepartmentList weChatDepartmentList = WeChatOperation.GetDepartment(token, weChatUserInfo.main_department); 
              if (weChatDepartmentList == null) throw new BusException("获取部门信息失败!");
              F_UserInfo fUserInfo = new F_UserInfo()
              {
                  Id = IdHelper.GetId(),
                  IsAdmin = false,
                  ShopInfoId = "",
                  UserName = weChatUserInfo.name,
                  UserImgUrl = weChatUserInfo.avatar,
                  WeCharUserId = weChatUserInfo.userid,
                  Department = weChatDepartmentList?.department?.FirstOrDefault()?.name,
                  CreateTime = DateTime.Now,
                  CreatorId = "system",
                  CreatorName = weChatUserInfo.name,
              };
              await InsertAsync(fUserInfo);
           }
           //生成token,有效期一天
           JWTPayload jWTPayload = new JWTPayload
           {
               UserId = userId,
               Expire = DateTime.Now.AddDays(1)
           };
           string tk = JWTHelper.GetToken(jWTPayload.ToJson(), JWTHelper.JWTClient);
           return tk;
        }

      #endregion

        #region 私有成员

        #endregion
    }
}