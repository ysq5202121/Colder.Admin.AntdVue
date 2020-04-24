using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerFood
{
    public interface IF_EvaluationBusiness
    {
        Task<PageResult<F_Evaluation>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<F_Evaluation> GetTheDataAsync(string id);
        Task AddDataAsync(F_Evaluation data);
        Task UpdateDataAsync(F_Evaluation data);
        Task DeleteDataAsync(List<string> ids);
    }
}