using Coldairarrow.Business.Dome;
using Coldairarrow.Entity.Dome;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Dome
{
    [Route("/Dome/[controller]/[action]")]
    public class D_SupplierManagerController : BaseApiController
    {
        #region DI

        public D_SupplierManagerController(ID_SupplierManagerBusiness d_SupplierManagerBus)
        {
            _d_SupplierManagerBus = d_SupplierManagerBus;
        }

        ID_SupplierManagerBusiness _d_SupplierManagerBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<D_SupplierManager>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _d_SupplierManagerBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<D_SupplierManager> GetTheData(IdInputDTO input)
        {
            return await _d_SupplierManagerBus.GetTheDataAsync(input.id);
        }
        [HttpPost]
        public async Task<SupplierManagerStatusDto> GetStatusList()
        {

            return await _d_SupplierManagerBus.GetStatusListAsync();
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(D_SupplierManager data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _d_SupplierManagerBus.AddDataAsync(data);
            }
            else
            {
                await _d_SupplierManagerBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _d_SupplierManagerBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}