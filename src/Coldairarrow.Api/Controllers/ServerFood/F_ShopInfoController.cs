using Coldairarrow.Business.ServerFood;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ServerFood
{
    [Route("/ServerFood/[controller]/[action]")]
    public class F_ShopInfoController : BaseApiController
    {
        #region DI

        public F_ShopInfoController(IF_ShopInfoBusiness f_ShopInfoBus)
        {
            _f_ShopInfoBus = f_ShopInfoBus;
        }

        IF_ShopInfoBusiness _f_ShopInfoBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<F_ShopInfo>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _f_ShopInfoBus.GetDataListAsync(input);
        }

        [HttpPost]
        [NoCheckJWT]
        [CheckJWTClient(AppId = (int)EnumWeChatAppType.Food)]
        public async Task<List<F_ShopInfo>> GetDataListToMoblie()
        {
            return await _f_ShopInfoBus.GetDataListToMoblieAsync();
        }

        [HttpPost]
        public async Task<F_ShopInfo> GetTheData(IdInputDTO input)
        {
            return await _f_ShopInfoBus.GetTheDataAsync(input.id);
        }

        [HttpPost]
        public async Task<List<F_ShopInfo>> GetDataListAll()
        {
            return await _f_ShopInfoBus.GetDataListAllAsync();
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(F_ShopInfo data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _f_ShopInfoBus.AddDataAsync(data);
            }
            else
            {
                InitUpdateEntity(data);
                await _f_ShopInfoBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        [ApiPermission("F_ShopInfo.Delete")]
        public async Task DeleteData(List<string> ids)
        {
            await _f_ShopInfoBus.DeleteDataAsync(ids);
        }

        [HttpGet]
        [NoCheckJWT]
        public async Task GetBarCode(string url,string id)
        {
            Response.ContentType = "image/jpeg";
            MemoryStream tempStream = new MemoryStream();
            QRCodeHelper.BuildQRCode(url + "/clientfood/scancode?id="+ id).Save(tempStream, ImageFormat.Png);
            await Response.Body.WriteAsync(tempStream.ToArray());
             ;
        }

        #endregion
    }
}