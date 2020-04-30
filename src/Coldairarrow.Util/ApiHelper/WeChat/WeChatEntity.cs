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
        public string CorpId { get; set; }
        public string CorpSecret { get; set; }
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

}
