using Coldairarrow.Business.ServerRoom;
using Coldairarrow.Entity.ServerRoom;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ServerRoom
{
    [Route("/ServerRoom/[controller]/[action]")]
    public class C_MakeConferenceRoomController : BaseApiController
    {
        #region DI

        public C_MakeConferenceRoomController(IC_MakeConferenceRoomBusiness c_MakeConferenceRoomBus)
        {
            _c_MakeConferenceRoomBus = c_MakeConferenceRoomBus;
        }

        IC_MakeConferenceRoomBusiness _c_MakeConferenceRoomBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<C_MakeConferenceRoom>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _c_MakeConferenceRoomBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<C_MakeConferenceRoom> GetTheData(IdInputDTO input)
        {
            return await _c_MakeConferenceRoomBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(C_MakeConferenceRoom data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _c_MakeConferenceRoomBus.AddDataAsync(data);
            }
            else
            {
                await _c_MakeConferenceRoomBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _c_MakeConferenceRoomBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}