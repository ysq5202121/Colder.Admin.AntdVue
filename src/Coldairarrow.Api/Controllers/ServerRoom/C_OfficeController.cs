using Coldairarrow.Business.ServerRoom;
using Coldairarrow.Entity.ServerRoom;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ServerRoom
{
    [Route("/ServerRoom/[controller]/[action]")]
    public class C_OfficeController : BaseApiController
    {
        #region DI

        public C_OfficeController(IC_OfficeBusiness c_OfficeBus)
        {
            _c_OfficeBus = c_OfficeBus;
        }

        IC_OfficeBusiness _c_OfficeBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<C_Office>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _c_OfficeBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<C_Office> GetTheData(IdInputDTO input)
        {
            return await _c_OfficeBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(C_Office data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _c_OfficeBus.AddDataAsync(data);
            }
            else
            {
                await _c_OfficeBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _c_OfficeBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}