﻿using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    public class Base_DictionaryController : BaseApiController
    {
        #region DI

        public Base_DictionaryController(IBase_DictionaryBusiness base_DictionaryBus)
        {
            _base_DictionaryBus = base_DictionaryBus;
        }

        IBase_DictionaryBusiness _base_DictionaryBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<Base_DictionaryResultDto>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _base_DictionaryBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<Base_Dictionary> GetTheData(IdInputDTO input)
        {
            return await _base_DictionaryBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(Base_Dictionary data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _base_DictionaryBus.AddDataAsync(data);
            }
            else
            {
                InitUpdateEntity(data);
                await _base_DictionaryBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _base_DictionaryBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}