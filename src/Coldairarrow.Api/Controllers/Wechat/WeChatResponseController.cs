using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using Coldairarrow.Util;
using Coldairarrow.Util.ApiHelper.WeChat;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Coldairarrow.Api.Controllers.Wechat
{
    [Route("api/[controller]")]
    [ApiController]
    [ModelMetadataType(typeof(XmlAnyAttributeAttribute))]
    public class WeChatResponseController : ControllerBase
    {
        public ILogger _log;
        public WeChatResponseController(ILogger<WeChatResponseController> log)
        {
            _log = log;
        }
        // GET: api/WeChatResponse
        [HttpGet]
        public Task Get(string msg_signature,string timestamp,string nonce,string echostr)
        {
            _log.LogInformation("开始执行");
            WeChatAuthInfo weChatAuthInfo= WeChatOperation.GetWeChatAuthInfo(EnumWeChatAppType.Food);
            WXBizMsgCrypt wxcpt = new WXBizMsgCrypt(weChatAuthInfo.Token, weChatAuthInfo.EncodingAESKey, weChatAuthInfo.CorpId);
            string sVerifyMsgSig = HttpUtility.UrlDecode(msg_signature);
            //string sVerifyMsgSig = "5c45ff5e21c57e6ad56bac8758b79b1d9ac89fd3";
             string sVerifyTimeStamp = HttpUtility.UrlDecode(timestamp);
            // string sVerifyTimeStamp = "1409659589";
             string sVerifyNonce = HttpUtility.UrlDecode(nonce);
            //string sVerifyNonce = "263014780";
             string sVerifyEchoStr = HttpUtility.UrlDecode(echostr);
             //string sVerifyEchoStr = "P9nAzCzyDtyTWESHep1vC5X9xho/qYX3Zpb4yKa9SKld1DsH3Iyt3tP3zNdtp+4RPcs8TgAE7OaBO+FZXvnaqQ==";
            int ret = 0;
            string sEchoStr = "";
            ret = wxcpt.VerifyURL(sVerifyMsgSig, sVerifyTimeStamp, sVerifyNonce, sVerifyEchoStr, ref sEchoStr);
            if (ret != 0)
            {
               _log.LogInformation(ret.ToString());
               return Task.CompletedTask;
            }
            _log.LogInformation(sEchoStr);
            Response.WriteAsync(sEchoStr);
            return  Task.CompletedTask;
        }

        // GET: api/WeChatResponse/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/WeChatResponse
        [HttpPost]
        public void Post([FromQuery]string msg_signature, [FromQuery]string timestamp, [FromQuery]string nonce, [FromBody]XmlDocument value)
        {

        }


        // PUT: api/WeChatResponse/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    [Serializable]
    public class xml
    {
        public string ToUserName { get; set; }
        public string AgentID { get; set; }
        public string Encrypt { get; set; }
    }
}
