using Coldairarrow.Business.ServerFood;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ServerFood
{
    [Route("/ServerFood/[controller]/[action]")]
    public class F_FoodInfoController : BaseApiController
    {
        #region DI

        public F_FoodInfoController(IF_FoodInfoBusiness f_FoodInfoBus)
        {
            _f_FoodInfoBus = f_FoodInfoBus;
        }

        IF_FoodInfoBusiness _f_FoodInfoBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<F_FoodInfoResultDto>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _f_FoodInfoBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<F_FoodInfo> GetTheData(IdInputDTO input)
        {
            return await _f_FoodInfoBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(F_FoodInfo data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _f_FoodInfoBus.AddDataAsync(data);
            }
            else
            {
                InitUpdateEntity(data);
                await _f_FoodInfoBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _f_FoodInfoBus.DeleteDataAsync(ids);
        }

        [HttpPost]
        [ApiPermission("F_FoodInfo.Delete")]
        public async Task PublishFoodData(List<string> ids)
        {
            await _f_FoodInfoBus.PublishFoodDataAsync(ids);
        }

        #endregion
    }
}