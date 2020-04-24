using Coldairarrow.Business.ServerFood;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ServerFood
{
    [Route("/ServerFood/[controller]/[action]")]
    public class F_EvaluationController : BaseApiController
    {
        #region DI

        public F_EvaluationController(IF_EvaluationBusiness f_EvaluationBus)
        {
            _f_EvaluationBus = f_EvaluationBus;
        }

        IF_EvaluationBusiness _f_EvaluationBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<F_Evaluation>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _f_EvaluationBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<F_Evaluation> GetTheData(IdInputDTO input)
        {
            return await _f_EvaluationBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(F_Evaluation data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _f_EvaluationBus.AddDataAsync(data);
            }
            else
            {
                await _f_EvaluationBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _f_EvaluationBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}