﻿using Coldairarrow.Business.ServerFood;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ServerFood
{
    [Route("/ServerFood/[controller]/[action]")]
    public class F_OrderController : BaseApiController
    {
        #region DI

        public F_OrderController(IF_OrderBusiness f_OrderBus)
        {
            _f_OrderBus = f_OrderBus;
        }

        IF_OrderBusiness _f_OrderBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<F_Order>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _f_OrderBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<F_Order> GetTheData(IdInputDTO input)
        {
            return await _f_OrderBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(F_Order data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _f_OrderBus.AddDataAsync(data);
            }
            else
            {
                await _f_OrderBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _f_OrderBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}