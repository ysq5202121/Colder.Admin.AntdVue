using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Coldairarrow.Api
{
    /// <summary>
    /// JWT校验
    /// </summary>
    public class CheckJWTClientAttribute : BaseActionFilterAsync
    {
        private  static readonly int _errorCode = 402;
        private  string JwtKey = JWTHelper.JWTClient;
        private  bool IsQuickDebug =ConfigHelper.GetValue("IsQuickDebug").ToBool(false);
        public int AppId=0;
 
        public override async Task OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                var req = context.HttpContext.Request;
                string token = req.GetToken();
                if (token.IsNullOrEmpty())
                {
                    context.Result = Error("缺少token", _errorCode, AppId);
                    return;
                }
                if (!JWTHelper.CheckToken(token, JwtKey) && !IsQuickDebug)
                {
                    context.Result = Error("token校验失败!", _errorCode, AppId);
                    return;
                }
                var payload = JWTHelper.GetPayload<JWTPayload>(token);
                if (payload.Expire < DateTime.Now)
                {
                    context.Result = Error("token过期!", _errorCode, AppId);
                    return;
                }
                if (AppId != payload.AppId && !IsQuickDebug)
                {
                    context.Result = Error("token访问了错误的的应用", _errorCode,AppId);
                    return;
                }
            }
            catch (Exception ex)
            {
                context.Result = Error(ex.Message, _errorCode, AppId);
            }

            await Task.CompletedTask;
        }
    }
}