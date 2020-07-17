using Coldairarrow.Business.ServerFood;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ServerFood
{
    [Route("/ServerFood/[controller]/[action]")]
    public class F_OrderController : BaseApiController
    {
        #region DI

        public F_OrderController(IF_OrderBusiness f_OrderBus)
        {
            _f_OrderBus = f_OrderBus;
        }

        IF_OrderBusiness _f_OrderBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<IF_OrderResultDTO>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _f_OrderBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<F_Order> GetTheData(IdInputDTO input)
        {
            return await _f_OrderBus.GetTheDataAsync(input.id);
        }

        [HttpPost]
        [NoCheckJWT]
        [CheckJWTClient(AppId = (int)EnumWeChatAppType.Food)]
        public async Task<List<IF_OrderResultDTO>> GetDataListToMoblie()
        {
            return await _f_OrderBus.GetDataListToMoblieAsync();
        }

        [HttpPost]
        public async Task ExcelToExport(ConditionDTO input)
        {
             var bytes= await _f_OrderBus.ExcelToExport(input);
             Response.ContentType = "application/vnd.ms-excel";
             await Response.Body.WriteAsync(bytes);
        }
        [HttpPost]
        public async Task SumExcelToExport(ConditionDTO input)
        {
            var bytes = await _f_OrderBus.SumExcelToExport(input);
            Response.ContentType = "application/vnd.ms-excel";
            await Response.Body.WriteAsync(bytes);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(F_Order data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _f_OrderBus.AddDataAsync(data);
            }
            else
            {
                InitUpdateEntity(data);
                await _f_OrderBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        [NoCheckJWT]
        [CheckJWTClient(AppId = (int)EnumWeChatAppType.Food)]
        public async Task PlaceOrderAsync(List<IF_OrderInputDTO> data) 
        {
            await _f_OrderBus.PlaceOrderAsync(data);
        }

        [HttpPost]
        [ApiPermission("F_Order.Delete")]
        public async Task DeleteData(List<string> ids)
        {
            await _f_OrderBus.DeleteDataAsync(ids);
        }

        [HttpGet]
        [NoCheckJWT]
        [CheckJWTClient(AppId = (int)EnumWeChatAppType.Food)]
        public async Task<bool> CancelOrder(string orderCode)
        {
           return await _f_OrderBus.CancelOrderAsync(orderCode);
        }


        #endregion
    }
}