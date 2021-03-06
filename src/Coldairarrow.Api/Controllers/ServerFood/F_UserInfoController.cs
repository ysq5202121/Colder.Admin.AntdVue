﻿using Coldairarrow.Business.ServerFood;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ServerFood
{
    [Route("/ServerFood/[controller]/[action]")]
    public class F_UserInfoController : BaseApiController
    {
        #region DI

        public F_UserInfoController(IF_UserInfoBusiness f_UserInfoBus)
        {
            _f_UserInfoBus = f_UserInfoBus;
        }

        IF_UserInfoBusiness _f_UserInfoBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<IF_UserInfoResultDto>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _f_UserInfoBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<F_UserInfo> GetTheData(IdInputDTO input)
        {
            return await _f_UserInfoBus.GetTheDataAsync(input.id);
        }
        [HttpPost]
        [NoCheckJWT]
        [CheckJWTClient(AppId = (int)EnumWeChatAppType.Food)]
        public async Task<F_UserInfo> GetUserInfoToMoblie()
        {

            return await _f_UserInfoBus.GetUserInfoToMoblieAsync();
        }
        [HttpPost]
        [NoCheckJWT]
        [CheckJWTClient(AppId = (int)EnumWeChatAppType.Food)]
        public async Task UpdateShopName(F_UserInfo data)
        {
             await _f_UserInfoBus.UpdateShopNameAsync(data);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(F_UserInfo data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _f_UserInfoBus.AddDataAsync(data);
            }
            else
            {
                InitUpdateEntity(data);
                await _f_UserInfoBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        [ApiPermission("F_UserInfo.Delete")]
        public async Task DeleteData(List<string> ids)
        {
            await _f_UserInfoBus.DeleteDataAsync(ids);
        }



        #endregion
    }
}