using System;
using System.Collections.Generic;
using System.Text;
using Coldairarrow.Business.ServerFood;
using Coldairarrow.Util;
using Coldairarrow.Util.ApiHelper.WeChat;
using EFCore.Sharding;

namespace Coldairarrow.Business.Job
{
    public  class JobServer: IJobServer, ITransientDependency
    { 
        
        public JobServer()
        {
 
        }

        public  void SendMsg( )
        {
           string token=  WeChatOperation.GetToken();
           WeChatOperation.SendMsg(token, "", "", "");

           //BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));
        }
    }
}
