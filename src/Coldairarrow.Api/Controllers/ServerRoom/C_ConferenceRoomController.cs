using Coldairarrow.Business.ServerRoom;
using Coldairarrow.Entity.ServerRoom;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ServerRoom
{
    [Route("/ServerRoom/[controller]/[action]")]
    public class C_ConferenceRoomController : BaseApiController
    {
        #region DI

        public C_ConferenceRoomController(IC_ConferenceRoomBusiness c_ConferenceRoomBus)
        {
            _c_ConferenceRoomBus = c_ConferenceRoomBus;
        }

        IC_ConferenceRoomBusiness _c_ConferenceRoomBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<C_ConferenceRoomResultDto>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _c_ConferenceRoomBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<C_ConferenceRoom> GetTheData(IdInputDTO input)
        {
            return await _c_ConferenceRoomBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(C_ConferenceRoom data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _c_ConferenceRoomBus.AddDataAsync(data);
            }
            else
            {
                await _c_ConferenceRoomBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _c_ConferenceRoomBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}