﻿using Coldairarrow.Business.ServerFood;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ServerFood
{
    [Route("/ServerFood/[controller]/[action]")]
    public class F_PublishFoodController : BaseApiController
    {
        #region DI

        public F_PublishFoodController(IF_PublishFoodBusiness f_PublishFoodBus)
        {
            _f_PublishFoodBus = f_PublishFoodBus;
        }

        IF_PublishFoodBusiness _f_PublishFoodBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<F_PublishFoodResultDto>> GetDataList(PageInput<F_PublishFoodInputDto> input)
        {
            return await _f_PublishFoodBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<F_PublishFood> GetTheData(IdInputDTO input)
        {
            return await _f_PublishFoodBus.GetTheDataAsync(input.id);
        }

        [HttpPost]
        [NoCheckJWT]
        [CheckJWTClient(AppId = (int)EnumWeChatAppType.Food)]
        public async Task<List<F_PublishFoodResultDto>> GetDataListToMobile(ConditionDTO input)
        {
            return await _f_PublishFoodBus.GetDataListToMoblieAsync(input);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(F_PublishFood data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _f_PublishFoodBus.AddDataAsync(data);
            }
            else
            {
                InitUpdateEntity(data);
                await _f_PublishFoodBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        [ApiPermission("F_PublishFood.Delete")]
        public async Task DeleteData(List<string> ids)
        {
            await _f_PublishFoodBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}