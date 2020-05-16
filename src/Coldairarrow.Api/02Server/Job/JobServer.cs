using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coldairarrow.Api.Controllers;
using Hangfire;
using Microsoft.AspNetCore.Builder;

namespace Coldairarrow.Api
{
    public static class JobServer
    {
        public static void UseHangfireAddServer(this IApplicationBuilder app)
        {
            //BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));
            //RecurringJob.AddOrUpdate(() => Console.Write("Easy!"), Cron.Hourly(30));
            //RecurringJob.AddOrUpdate(()=>aaa(), Cron.Hourly(40));
      
        }

        public static void aaa()
        {
            Console.Write("Easy!");
        }
    }
}
