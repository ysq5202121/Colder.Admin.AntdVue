using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Coldairarrow.Util;
using Hangfire.Annotations;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Microsoft.Owin;

namespace Coldairarrow.Api
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private static readonly int _errorCode = 401;
        public string JwtKey = JWTHelper.JWTSecret;
        public bool Authorize([NotNull] DashboardContext context)
        {

            var req = context.GetHttpContext().Request;
            string token = req.Query["token"].ToString();
            if (token.IsNullOrEmpty())
            {
                // 获取cookie
                token = req.Cookies["Authorization"];
            }
            try
            {

                if (token.IsNullOrEmpty())
                {
                    //context.Result = Error("缺少token", _errorCode);
                    return false;
                }

                if (!JWTHelper.CheckToken(token, JwtKey))
                {
                    //context.Result = Error("token校验失败!", _errorCode);
                    return false;
                }

                var payload = JWTHelper.GetPayload<JWTPayload>(token);
                if (payload.Expire < DateTime.Now)
                {
                    //context.Result = Error("token过期!", _errorCode);
                    return false;
                }
            }
            catch (Exception ex)
            {
                //context.Result = Error(ex.Message, _errorCode);
                return false;
            }
            //设置cookie
            context.GetHttpContext().Response.Cookies.Append("Authorization", token);
            return true;
        }

        public static string GetPara(string url, string name)
        {
            Regex reg = new Regex(@"(?:^|\?|&)" + name + "=(?<VAL>.+?)(?:&|$)");
            Match m = reg.Match(url);
            return m.Groups["VAL"].ToString(); ;
        }
    }
}
