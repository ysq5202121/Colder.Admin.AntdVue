using Coldairarrow.Business.ServerFood;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ServerFood
{
    [Route("/ServerFood/[controller]/[action]")]
    public class F_OrderInfoController : BaseApiController
    {
        #region DI

        public F_OrderInfoController(IF_OrderInfoBusiness f_OrderInfoBus)
        {
            _f_OrderInfoBus = f_OrderInfoBus;
        }

        IF_OrderInfoBusiness _f_OrderInfoBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<F_OrderInfo>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _f_OrderInfoBus.GetDataListAsync(input);
        }
        [HttpPost]
        public async Task<List<IF_OrderInfoResultDto>> GetDataListNoPage(ConditionDTO input)
        {
            return await _f_OrderInfoBus.GetDataListNoPageAsync(input);
        }

        [HttpPost]
        public async Task<F_OrderInfo> GetTheData(IdInputDTO input)
        {
            return await _f_OrderInfoBus.GetTheDataAsync(input.id);
        }
        [HttpPost]
        [NoCheckJWT]
        [CheckJWTClient]
        public async Task<List<IF_OrderInfoResultDto>> GetDataListToMoblie(ConditionDTO input)
        {
            return await _f_OrderInfoBus.GetDataListToMoblieAsync(input);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(F_OrderInfo data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _f_OrderInfoBus.AddDataAsync(data);
            }
            else
            {
                await _f_OrderInfoBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _f_OrderInfoBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}