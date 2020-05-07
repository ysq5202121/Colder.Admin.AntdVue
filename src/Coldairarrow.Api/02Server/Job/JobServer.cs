using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Builder;

namespace Coldairarrow.Api
{
    public static class JobServer
    {
        public static void UseHangfireAddServer(this IApplicationBuilder app)
        {

           // BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));
            //RecurringJob.AddOrUpdate(() => Console.Write("Easy!"), Cron.Hourly(30));
        }
    }
}
