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
        public string _jwtKey = JWTHelper.JWTSecret;
        public bool Authorize([NotNull] DashboardContext context)
        {

            var req = context.GetHttpContext().Request;
            string token = req.Query["token"].ToString();
            if (token.IsNullOrEmpty())
            {
                // 获取cookie
                token = req.Cookies["Authorization"];
            }
            else
            {
                //设置cookie
                context.GetHttpContext().Response.Cookies.Append("Authorization", token);
            }
            try
            { 
                if (token.IsNullOrEmpty())
                {
                    return false;
                }

                if (!JWTHelper.CheckToken(token, _jwtKey))
                {
                    return false;
                }

                var payload = JWTHelper.GetPayload<JWTPayload>(token);
                if (payload.Expire < DateTime.Now)
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
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
