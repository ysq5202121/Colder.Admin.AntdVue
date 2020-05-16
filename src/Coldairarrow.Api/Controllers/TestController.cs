using Coldairarrow.Entity.Base_Manage;
using EFCore.Sharding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Coldairarrow.Util;
using Coldairarrow.Util.ApiHelper.WeChat;
using Dynamitey;
using Microsoft.AspNetCore.Http;


namespace Coldairarrow.Api.Controllers
{
    [Route("/[controller]/[action]")]
    public class TestController : BaseController
    {
        readonly IRepository _repository;
        public TestController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task PressTest()
        {
            Base_User base_User = new Base_User
            {
                Id = Guid.NewGuid().ToString(),
                Birthday = DateTime.Now,
                CreateTime = DateTime.Now,
                CreatorId = Guid.NewGuid().ToString(),
                DepartmentId = Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                RealName = Guid.NewGuid().ToString(),
                Sex = Sex.Man,
                UserName = Guid.NewGuid().ToString()
            };

            await _repository.InsertAsync(base_User);
            await _repository.UpdateAsync(base_User);
            await _repository.GetIQueryable<Base_User>().Where(x => x.Id == base_User.Id).FirstOrDefaultAsync();
            await _repository.DeleteAsync(base_User);
        }
        [HttpPost]
        public async Task ExcelExport()
        {
            DataTable dt=new DataTable();
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("age", typeof(int));
            DataRow dr=  dt.NewRow();
            dr["name"] = "111";
            dr["age"] = 11;
            dt.Rows.Add(dr);
            HttpContext.Response.Clear();
            // HttpContext.Response.Buffer = true;
            // HttpContext.Response.c = "utf-8";
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Headers", Request.Headers["Access-Control-Request-Headers"]);
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            HttpContext.Response.ContentType = "application/vnd.ms-excel";
            byte[] b= AsposeOfficeHelper.DataTableToExcelBytes(dt);
            await Response.Body.WriteAsync(b);
        }
        [HttpGet]
        public async Task ExcelExports()
        {
           
            //await Response.Body.WriteAsync(b);
            await Task.CompletedTask;
        }

        [HttpGet]
        public async Task SendMsg(string token, string userList, string agentid, string msgContent)
        {

            //WeChatOperation.SendMsg(token, userList, EnumWeChatAppType.Food, msgContent);
            await Task.CompletedTask;
        }
    }
}