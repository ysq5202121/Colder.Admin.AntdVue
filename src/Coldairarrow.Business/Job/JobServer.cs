using System;
using System.Collections.Generic;
using System.Text;
using Coldairarrow.Business.ServerFood;
using Coldairarrow.Util;
using Coldairarrow.Util.ApiHelper.WeChat;
using EFCore.Sharding;
using Hangfire;

namespace Coldairarrow.Business.Job
{
    public  class JobServer: IJobServer, ITransientDependency
    { 
        
        public JobServer()
        {
 
        }

        public void test()
        {
            //支持基于队列的任务处理：任务执行不是同步的，而是放到一个持久化队列中，以便马上把请求控制权返回给调用者。
            var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("队列任务执行了！"));
            //延迟任务执行：不是马上调用方法，而是设定一个未来时间点再来执行，延迟作业仅执行一次
            var jobId1 = BackgroundJob.Schedule(() => Console.WriteLine("一天后的延迟任务执行了！"),TimeSpan.FromDays(1));//一天后执行该任务
          
            //循环任务执行：一行代码添加重复执行的任务，其内置了常见的时间循环模式，也可基于CRON表达式来设定复杂的模式。【用的比较的多】
            RecurringJob.AddOrUpdate(() => Console.WriteLine("每分钟执行任务！"), Cron.Minutely); //注意最小单位是分钟

            //延续性任务执行：类似于.NET中的Task,可以在第一个任务执行完之后紧接着再次执行另外的任务
            BackgroundJob.ContinueWith(jobId, () => Console.WriteLine("连续任务！"));

        }

        public  void SendMsg()
        {
            //string token=  WeChatOperation.GetToken();
            //WeChatOperation.SendMsg(token, "", "", "");
            //DateTime.
            BackgroundJob.Schedule(() => Console.WriteLine("一天后的延迟任务执行了！"), DateTime.Now.AddMinutes(3).TimeOfDay);
        }

        public void ss()
        {

        }
    }
}
