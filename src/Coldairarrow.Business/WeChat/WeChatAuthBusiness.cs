using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Coldairarrow.Business.ServerFood;
using Coldairarrow.Business.ServerReport;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.IBusiness.WeChat;
using Coldairarrow.Util;
using EFCore.Sharding;

namespace Coldairarrow.Business.WeChat
{
    public class WeChatAuthBusiness : BaseBusiness<F_UserInfo>, IWeChatAuthBusiness, ITransientDependency
    {
        private IF_UserInfoBusiness _userInfoBusiness;
        private IRP_UserInfoBusiness _rpUserInfoBusiness;

        public WeChatAuthBusiness(IRepository repository, IF_UserInfoBusiness _fUserInfo,IRP_UserInfoBusiness _rpUserInfo)
            : base(repository)
        {
            _userInfoBusiness = _fUserInfo;
            _rpUserInfoBusiness = _rpUserInfo;
        }

        public async Task<string> Login(string code, int appID)
        {
            string token = string.Empty;
            switch (appID)
            {
                case (int) EnumWeChatAppType.Food:
                    token = await _userInfoBusiness.Login(code);
                    break;
                case (int) EnumWeChatAppType.Room:
                    break;
                case (int) EnumWeChatAppType.Report:
                    token = await _rpUserInfoBusiness.Login(code);
                    break;
                default:
                    break;
            }
            return token;
        }

    }
}
