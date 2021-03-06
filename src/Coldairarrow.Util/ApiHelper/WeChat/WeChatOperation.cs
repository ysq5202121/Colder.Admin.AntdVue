﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EFCore.Sharding.Util;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Coldairarrow.Util.ApiHelper.WeChat
{
   public  class WeChatOperation
   {
       const string GetTokenUrl = "https://qyapi.weixin.qq.com/cgi-bin/gettoken";
       const string GetCodeUrl = "https://open.weixin.qq.com/connect/oauth2/authorize";
       const string GetUserIdUrl = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo";
       const string GetUserInfoUrl = "https://qyapi.weixin.qq.com/cgi-bin/user/get";
       const string GetDepartmentUrl = "https://qyapi.weixin.qq.com/cgi-bin/department/list";
       const string SendMssage = " https://qyapi.weixin.qq.com/cgi-bin/message/send";//发送消息接口
       public static List<WeChatAuthInfo> autoInfoList = ConfigHelper.Configuration.GetSection("WeChatAuth").Get<List<WeChatAuthInfo>>();
       public static ILogger logger;

       /// <summary>
       /// 获取配置信息
       /// </summary>
       /// <param name="app"></param>
       /// <returns></returns>
       public static WeChatAuthInfo GetWeChatAuthInfo(EnumWeChatAppType app)
       {
           return autoInfoList.FirstOrDefault(a => a.AppId == (int) app);
       }

       public WeChatOperation(ILogger<WeChatOperation> _logger)
       {
           logger = _logger;
       }

       public static string GetCode()
       {
           try
           {

               Dictionary<string, object> dic = new Dictionary<string, object>
               {
                   {"appid", autoInfoList.FirstOrDefault(a => a.AppId == (int) EnumWeChatAppType.Food)?.CorpId},
                   {"redirect_uri", autoInfoList.FirstOrDefault(a => a.AppId == (int)EnumWeChatAppType.Food)?.Url},
                   {"response_type", "code"},
                   {"scope", "snsapi_base"},
                   {"state", "STATE#wechat_redirect"},
               };
               string resultJson = HttpHelper.GetData(GetCodeUrl, dic);
               WeChatTokenReuslt token = resultJson.ToObject<WeChatTokenReuslt>();
               if (token.errcode != "0")
               {
                   LogHelper.WriteLog_LocalTxt(resultJson);
                   logger?.LogWarning(resultJson);
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
        /// 获取token
        /// </summary>
        /// <returns></returns>
        public static string GetToken(EnumWeChatAppType app)
        {
            try
            {

                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "corpid", autoInfoList.FirstOrDefault(a=>a.AppId==(int)app)?.CorpId },
                    { "corpsecret", autoInfoList.FirstOrDefault(a=>a.AppId==(int)app)?.CorpSecret }
                };
                string resultJson = HttpHelper.GetData(GetTokenUrl, dic);
                WeChatTokenReuslt token = resultJson.ToObject<WeChatTokenReuslt>();
                if (token.errcode != "0")
                {
                    LogHelper.WriteLog_LocalTxt(resultJson);
                    logger?.LogWarning(resultJson);
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
        /// <param name="token"></param>
        /// <returns></returns>
        public static string GetUserId(string code, string token)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "access_token", token },
                    { "code", code }
                };
                string resultJson = HttpHelper.GetData(GetUserIdUrl, dic);
                WeChatUserIdInfo userIdInfo = resultJson.ToObject<WeChatUserIdInfo>();
                if (userIdInfo.errcode != "0")
                {
                    LogHelper.WriteLog_LocalTxt(resultJson);
                    logger?.LogWarning(resultJson);
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
        public static WeChatUserInfo GetUserInfo(EnumWeChatAppType app,string code)
        {
            try
            {
                string token = GetToken(app);
                string userId = GetUserId(code, token);
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "access_token", token },
                    { "userid", userId }
                };
                string resultJson = HttpHelper.GetData(GetUserInfoUrl, dic);
                WeChatUserInfo userIdInfo = resultJson.ToObject<WeChatUserInfo>();
                if (userIdInfo.errcode != "0")
                {
                    LogHelper.WriteLog_LocalTxt(resultJson);
                    logger?.LogWarning(resultJson);
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
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "access_token", token },
                    { "userid", userid }
                };
                string resultJson = HttpHelper.GetData(GetUserInfoUrl, dic);
                WeChatUserInfo userIdInfo = resultJson.ToObject<WeChatUserInfo>();
                if (userIdInfo.errcode != "0")
                {
                    LogHelper.WriteLog_LocalTxt(resultJson);
                    logger?.LogWarning(resultJson);
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
        /// <param name="token">token</param>
        /// <param name="departmentId">部门ID</param>
        /// <returns></returns>
        public static WeChatDepartmentList GetDepartment(string token, string departmentId)
        {
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "access_token", token },
                    { "id", departmentId }
                };
                string resultJson = HttpHelper.GetData(GetDepartmentUrl, dic);
                WeChatDepartmentList departmentList = resultJson.ToObject<WeChatDepartmentList>();
                if (departmentList.errcode != "0")
                {
                    LogHelper.WriteLog_LocalTxt(resultJson);
                    logger?.LogWarning(resultJson);
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

        /// <summary>
        /// 发送企业微信消息
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="userList">指定接收消息的成员，成员ID列表（多个接收者用‘|’分隔，最多支持1000个）。特殊情况：指定为”@all”，则向该企业应用的全部成员发送</param>
        /// <param name="agentid">企业应用的id，整型。企业内部开发，可在应用的设置页面查看</param>
        /// <param name="msgContent">消息内容，最长不超过2048个字节，超过将截断（支持id转译）</param>
        /// <returns></returns>
        public static bool SendMsg(string token,string userList, EnumWeChatAppType app, WeChatSendMsgContext weChatSendMsgContext)
        {
            try
            {
                WeChatSendMsg weChatSendMsg=new WeChatSendMsg();
                weChatSendMsgContext.url = autoInfoList.FirstOrDefault(a => a.AppId == (int) app)?.Url + weChatSendMsgContext.url;
                Dictionary<string, object> dic = new Dictionary<string, object>
                {
                    { "touser", userList },
                    { "msgtype", "textcard" },
                    { "agentid", autoInfoList.FirstOrDefault(a=>a.AppId==(int)app)?.AgentId },
                    { "textcard", weChatSendMsgContext }
                };
                string url = SendMssage + "?access_token=" + token;
                string resultJson = HttpHelper.PostData(url, dic,contentType:ContentType.Json);
                WeChatSendMsgResult weChatSendMsgResult = resultJson.ToObject<WeChatSendMsgResult>();
                if (weChatSendMsgResult.errcode != "0")
                {
                    LogHelper.WriteLog_LocalTxt(resultJson);
                    logger?.LogWarning(resultJson);
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                LogHelper.WriteLog_LocalTxt(e.Message);
                return false;
            }

        }
        

   }
}
