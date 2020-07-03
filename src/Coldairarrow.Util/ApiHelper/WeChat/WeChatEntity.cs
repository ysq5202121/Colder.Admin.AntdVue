using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.Util
{

    public class WeChatEntity
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }
    public class WeChatTokenReuslt: WeChatEntity
    {
        public string access_token { get; set; }
    }
    public class WeChatAuthInfo
    {
        public int AppId { get; set; }
        public string CorpId { get; set; }

        public string Token { get; set; }

        public string EncodingAESKey { get; set; }

        public string CorpSecret { get; set; }
        public string AgentId { get; set; }
        public string Url { get; set; }
    }

    public enum EnumWeChatAppType
    {
        Food=1,
        Room=2
    }
    public class WeChatUserIdInfo : WeChatEntity
    {
        public string UserId { get; set; }
        public string DeviceId { get; set; }

    }
    public class WeChatUserInfo : WeChatEntity
    {
        public string userid { get; set; }
        public string name { get; set; }

        public string[] department { get; set; }

        public string avatar { get; set; }

        public string thumb_avatar { get; set; }

        public string telephone { get; set; }

        public string gender { get; set; }

        public string open_userid { get; set; }

        public string main_department { get; set; }
    }
    public class WeChatDepartment
    {
        public string id { get; set; }

        public string name { get; set; }

        public string name_en { get; set; }

        public string parentid { get; set; }

        public string order { get; set; }
    }
    public class WeChatDepartmentList : WeChatEntity
    {
        public List<WeChatDepartment> department { get; set; }
    }

    public class WeChatSendMsgResult: WeChatEntity
    {
        public  string invaliduser { get; set; }

        public string invalidparty { get; set; }

        public string invalidtag { get; set; }

    }

    public class WeChatSendMsgContext
    { 
        public string title { get; set; }

        public string description { get; set; }

        public string url { get; set; }

        public string btntxt { get; set; }

    }

    public class WeChatSendMsg
    {
        public string touser { get; set; }

        public string toparty { get; set; }

        public string totag { get; set; }

        public string msgtype { get; set; }

        public string agentid { get; set; }

        public WeChatSendMsgContext text { get; set; }

        public string safe { get; set; }

        public string enable_id_trans { get; set; }

        public string enable_duplicate_check { get; set; }

        public string duplicate_check_interval { get; set; }


    }
}
