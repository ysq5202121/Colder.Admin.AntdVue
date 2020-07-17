using Coldairarrow.Business.ServerReport;
using Coldairarrow.Entity.ServerReport;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ServerReport
{
    [Route("/ServerReport/[controller]/[action]")]
    public class RP_UserInfoController : BaseApiController
    {
        #region DI

        public RP_UserInfoController(IRP_UserInfoBusiness rP_UserInfoBus)
        {
            _rP_UserInfoBus = rP_UserInfoBus;
        }

        IRP_UserInfoBusiness _rP_UserInfoBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<RP_UserInfo>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _rP_UserInfoBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<RP_UserInfo> GetTheData(IdInputDTO input)
        {
            return await _rP_UserInfoBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(RP_UserInfo data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _rP_UserInfoBus.AddDataAsync(data);
            }
            else
            {
                await _rP_UserInfoBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _rP_UserInfoBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}