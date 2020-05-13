using Coldairarrow.Business.ServerRoom;
using Coldairarrow.Entity.ServerRoom;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ServerRoom
{
    [Route("/ServerRoom/[controller]/[action]")]
    public class C_AttendeesController : BaseApiController
    {
        #region DI

        public C_AttendeesController(IC_AttendeesBusiness c_AttendeesBus)
        {
            _c_AttendeesBus = c_AttendeesBus;
        }

        IC_AttendeesBusiness _c_AttendeesBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<C_Attendees>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _c_AttendeesBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<C_Attendees> GetTheData(IdInputDTO input)
        {
            return await _c_AttendeesBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(C_Attendees data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _c_AttendeesBus.AddDataAsync(data);
            }
            else
            {
                await _c_AttendeesBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _c_AttendeesBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}