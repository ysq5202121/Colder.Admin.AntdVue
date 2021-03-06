﻿using Coldairarrow.Business.Dome;
using Coldairarrow.Entity.Dome;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Dome
{
    [Route("/Dome/[controller]/[action]")]
    public class D_SupplierContactsController : BaseApiController
    {
        #region DI

        public D_SupplierContactsController(ID_SupplierContactsBusiness d_SupplierContactsBus)
        {
            _d_SupplierContactsBus = d_SupplierContactsBus;
        }

        ID_SupplierContactsBusiness _d_SupplierContactsBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<D_SupplierContacts>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _d_SupplierContactsBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<D_SupplierContacts> GetTheData(IdInputDTO input)
        {
            return await _d_SupplierContactsBus.GetTheDataAsync(input.id);
        }
        [HttpPost]
        public async Task<List<D_SupplierContacts>> GetDataListById(ConditionDTO input)
        {
            return await _d_SupplierContactsBus.GetDataListByIdAsync(input);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(D_SupplierContacts data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _d_SupplierContactsBus.AddDataAsync(data);
            }
            else
            {
                await _d_SupplierContactsBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task SaveDataList(List<D_SupplierContacts> data)
        {
            var queryInsert = data.Where(a => a.Id.IsNullOrEmpty()).ToList();
            var queryUpdate = data.Where(a => !(a.Id.IsNullOrEmpty())).ToList();
            if (queryUpdate.Count > 0)
            {
                queryUpdate.ForEach(a =>
                {
                    InitUpdateEntity(a);
                });
                await _d_SupplierContactsBus.UpdateDataAsync(queryUpdate);
                await _d_SupplierContactsBus.DeleteDataAsync(queryUpdate);
            }
            if (queryInsert.Count > 0)
            {
                queryInsert.ForEach(a =>
                {
                    InitEntity(a);
                });
                await _d_SupplierContactsBus.AddDataAsync(queryInsert);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _d_SupplierContactsBus.DeleteDataAsync(ids);
        }
        [HttpPost]
        public async Task SetDefault(D_SupplierContacts data)
        {
            await _d_SupplierContactsBus.SetDefaultAsync(data);
        }

        #endregion
    }
}