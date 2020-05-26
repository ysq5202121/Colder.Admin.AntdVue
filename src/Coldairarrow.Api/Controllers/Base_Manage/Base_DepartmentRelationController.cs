using Coldairarrow.Business.Base_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Base_Manage
{
    [Route("/Base_Manage/[controller]/[action]")]
    public class Base_DepartmentRelationController : BaseApiController
    {
        #region DI

        public Base_DepartmentRelationController(IBase_DepartmentRelationBusiness base_DepartmentRelationBus)
        {
            _base_DepartmentRelationBus = base_DepartmentRelationBus;
        }

        IBase_DepartmentRelationBusiness _base_DepartmentRelationBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<Base_DepartmentRelation>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _base_DepartmentRelationBus.GetDataListAsync(input);
        }
        [HttpPost]
        public async Task<List<string>> GetNewDepartmentList()
        {
            return await _base_DepartmentRelationBus.GetNewDepartmentListAsync();
        }

        [HttpPost]
        public async Task<Base_DepartmentRelation> GetTheData(IdInputDTO input)
        {
            return await _base_DepartmentRelationBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(Base_DepartmentRelation data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _base_DepartmentRelationBus.AddDataAsync(data);
            }
            else
            {
                await _base_DepartmentRelationBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _base_DepartmentRelationBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}