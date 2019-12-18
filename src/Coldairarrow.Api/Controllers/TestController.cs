﻿using Coldairarrow.Business.Base_Manage;
using Coldairarrow.DataRepository;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers
{
    [Route("/[controller]/[action]")]
    public class TestController : BaseController
    {
        private Base_Log GetNewLog()
        {
            return new Base_Log
            {
                Id = IdHelper.GetId(),
                CreateTime = DateTime.Now
            };
        }

        /// <summary>
        /// 压力测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PressTest()
        {
            var bus = AutofacHelper.GetScopeService<IBase_UserBusiness>();
            var db = DbFactory.GetRepository();
            Base_UnitTest data = new Base_UnitTest
            {
                Id = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                Age = 10,
                UserName = Guid.NewGuid().ToString()
            };
            db.Insert(data);
            db.Update(data);
            db.GetIQueryable<Base_UnitTest>().FirstOrDefault();
            db.Delete(data);

            return Success("");
        }
    }
}