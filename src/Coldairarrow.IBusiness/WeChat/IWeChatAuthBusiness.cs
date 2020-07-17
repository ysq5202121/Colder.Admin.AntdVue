using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.IBusiness.WeChat
{
    public interface IWeChatAuthBusiness
    {
         Task<string> Login(string code, int appID);
    }
}
