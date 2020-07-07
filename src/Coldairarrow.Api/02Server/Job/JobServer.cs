using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coldairarrow.Api.Controllers;
using Coldairarrow.Business.ServerFood;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Coldairarrow.Api
{
    public static class JobServer
    {
        public static void UseHangfireAddServer(this IApplicationBuilder app)
        {
            //循环任务执行：一行代码添加重复执行的任务，其内置了常见的时间循环模式，也可基于CRON表达式来设定复杂的模式。【用的比较的多】
            //一周执行一次
            var f_UserInfoBus = app.ApplicationServices.GetService<IF_UserInfoBusiness>();
            RecurringJob.AddOrUpdate(() => f_UserInfoBus.TimedRefreshDepartment(), Cron.Weekly(DayOfWeek.Sunday)); //注意最小单位是分钟
            var fOrderBusiness = app.ApplicationServices.GetService<IF_OrderBusiness>();
            RecurringJob.AddOrUpdate(() => fOrderBusiness.RefreshOrderStatus(), Cron.Daily(23)); //注意最小单位是分钟

        }
    }
}
