using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Coldairarrow.Util.ApiHelper.WeChat
{
   public  class WeChatOperation
   {
       const string GetTokenUrl = "https://qyapi.weixin.qq.com/cgi-bin/gettoken";
       const string GetCodeUrl = "https://open.weixin.qq.com/connect/oauth2/authorize";
       const string GetUserIdUrl = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo";
       const string GetUserInfoUrl = "https://qyapi.weixin.qq.com/cgi-bin/user/get";
       const string GetDepartmentUrl = "https://qyapi.weixin.qq.com/cgi-bin/department/list";

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        public static string GetToken()
        {
            try
            {
                WeChatAuthInfo autoInfo = ConfigHelper.Configuration.GetSection("WeChatAuth").Get<WeChatAuthInfo>();
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("corpid", autoInfo.CorpId);
                dic.Add("corpsecret", autoInfo.CorpSecret);
                string resultJson = HttpHelper.GetData(GetTokenUrl, dic);
                WeChatTokenReuslt token = resultJson.ToObject<WeChatTokenReuslt>();
                if (token.errcode != "0")
                {
                    LogHelper.WriteLog_LocalTxt(resultJson);
                    return null;
                }
                return token.access_token;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog_LocalTxt(e.Message);
                return null;
            }
        }
        /// <summary>
        /// 获取Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetUserId(string code, string token)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("access_token", token);
                dic.Add("code", code);
                string resultJson = HttpHelper.GetData(GetUserIdUrl, dic);
                WeChatUserIdInfo userIdInfo = resultJson.ToObject<WeChatUserIdInfo>();
                if (userIdInfo.errcode != "0")
                {
                    LogHelper.WriteLog_LocalTxt(resultJson);
                    return null;
                }
                return userIdInfo.UserId;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog_LocalTxt(e.Message);
                return null;
            }
        }
        /// <summary>
        /// 根据Code获取用户信息
        /// </summary>
        /// <returns></returns>
        public static WeChatUserInfo GetUserInfo(string code)
        {
            try
            {
                string token = GetToken();
                string userId = GetUserId(code, token);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("access_token", token);
                dic.Add("userid", userId);
                string resultJson = HttpHelper.GetData(GetUserInfoUrl, dic);
                WeChatUserInfo userIdInfo = resultJson.ToObject<WeChatUserInfo>();
                if (userIdInfo.errcode != "0")
                {
                    LogHelper.WriteLog_LocalTxt(resultJson);
                    return null;
                }
                return userIdInfo;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog_LocalTxt(e.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public static WeChatUserInfo GetUserInfo(string token,string userid)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("access_token", token);
                dic.Add("userid", userid);
                string resultJson = HttpHelper.GetData(GetUserInfoUrl, dic);
                WeChatUserInfo userIdInfo = resultJson.ToObject<WeChatUserInfo>();
                if (userIdInfo.errcode != "0")
                {
                    LogHelper.WriteLog_LocalTxt(resultJson);
                    return null;
                }
                return userIdInfo;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog_LocalTxt(e.Message);
                return null;
            }
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        public static WeChatDepartmentList GetDepartment(string token, string departmentId)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("access_token", token);
                dic.Add("id", departmentId);
                string resultJson = HttpHelper.GetData(GetDepartmentUrl, dic);
                WeChatDepartmentList departmentList = resultJson.ToObject<WeChatDepartmentList>();
                if (departmentList.errcode != "0")
                {
                    LogHelper.WriteLog_LocalTxt(resultJson);
                    return null;
                }
                return departmentList;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog_LocalTxt(e.Message);
                return null;
            }
        }
    }
}
