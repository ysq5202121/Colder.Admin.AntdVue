using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coldairarrow.IBusiness.WeChat;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Coldairarrow.Api.Controllers.Wechat
{
    [Route("/Wechat/[controller]/[action]")]
    public class WeChatAuthController : BaseApiController
    {
        IWeChatAuthBusiness _weChatAuthBus { get; }
        public WeChatAuthController(IWeChatAuthBusiness _fWeChatAuth)
        {
            _weChatAuthBus = _fWeChatAuth;
        }
        [HttpGet]
        [NoCheckJWT]
        public async Task<string> Login(string code,int id)
        { 
            return  await _weChatAuthBus.Login(code, id);
        }

        //[HttpGet]
        //[NoCheckJWT]
        //public async Task<bool> CheckLogin(string code)
        //{
        //    return await _weChatAuthBus.CheckLogin(code);
        //}


    }
}