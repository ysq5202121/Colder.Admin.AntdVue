using System;
using System.Collections.Generic;
using System.Linq;
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
                token = req.GetToken();
            }
            if (req.Method == "POST")
            {
                return true;
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
            //context.GetHttpContext().Response.Headers.Add(new KeyValuePair<string, StringValues>("Authorization", "Bearer "+ token));
            return true;
        }
    }
}
