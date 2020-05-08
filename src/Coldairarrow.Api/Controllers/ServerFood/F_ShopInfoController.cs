using Coldairarrow.Business.ServerFood;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        [CheckJWTClient]
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
                await _f_ShopInfoBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _f_ShopInfoBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}