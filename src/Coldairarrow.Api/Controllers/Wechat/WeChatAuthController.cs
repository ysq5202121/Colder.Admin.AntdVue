using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coldairarrow.Business.ServerFood;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Coldairarrow.Api.Controllers.Wechat
{
    [Route("/Wechat/[controller]/[action]")]
    public class WeChatAuthController : BaseApiController
    {
        IF_UserInfoBusiness _f_UserInfoBus { get; }
        public WeChatAuthController(IF_UserInfoBusiness f_UserInfoBus)
        {
            _f_UserInfoBus = f_UserInfoBus;
        }
        [HttpGet]
        [NoCheckJWT]
        public async Task<string> Login(string code)
        { 
            return  await _f_UserInfoBus.Login(code);
        }

        [HttpGet]
        [NoCheckJWT]
        public async Task<bool> CheckLogin(string code)
        {
            return await _f_UserInfoBus.CheckLogin(code);
        }


    }
}