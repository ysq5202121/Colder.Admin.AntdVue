using Coldairarrow.Business.ServerFood;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ServerFood
{
    [Route("/ServerFood/[controller]/[action]")]
    public class F_ShopInfoSetController : BaseApiController
    {
        #region DI

        public F_ShopInfoSetController(IF_ShopInfoSetBusiness f_ShopInfoSetBus)
        {
            _f_ShopInfoSetBus = f_ShopInfoSetBus;
        }

        IF_ShopInfoSetBusiness _f_ShopInfoSetBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<IF_ShopInfoSetResultDTO>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _f_ShopInfoSetBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<F_ShopInfoSet> GetTheData(IdInputDTO input)
        {
            return await _f_ShopInfoSetBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(F_ShopInfoSet data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _f_ShopInfoSetBus.AddDataAsync(data);
            }
            else
            {
                InitUpdateEntity(data);
                await _f_ShopInfoSetBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        [ApiPermission("F_ShopInfo.Delete")]
        public async Task DeleteData(List<string> ids)
        {
            await _f_ShopInfoSetBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}