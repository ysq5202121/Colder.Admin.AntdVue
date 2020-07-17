using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Coldairarrow.Util
{
    
    public class EDOperation
    {
        const  string VerifyUserUrl = "";
        public static ILogger logger;

        public EDOperation(ILogger<EDOperation> _logger)
        {
            logger = _logger;
        }

        public static bool VerifyUser()
        {
            try
            {
                Dictionary<string, object> dicParamters = new Dictionary<string, object>();
                dicParamters.Add("", "");
                string resultJson = HttpHelper.GetData(VerifyUserUrl, dicParamters);
                var verifyUserResult = resultJson.ToObject<VerifyUserResult>();
                return verifyUserResult.IsSuccess;
            }
            catch (Exception e)
            {
                logger?.LogWarning(e.Message);
                return false;
            }
        }

        public string GetXXX()
        {
            return "";
        }


    }

}
