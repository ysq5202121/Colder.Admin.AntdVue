using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerFood
{
    public interface IF_PublishFoodBusiness
    {
        Task<PageResult<F_PublishFood>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<List<F_PublishFood>> GetDataListToMoblieAsync(ConditionDTO input);
        Task<F_PublishFood> GetTheDataAsync(string id);
        Task AddDataAsync(F_PublishFood data);
        Task UpdateDataAsync(F_PublishFood data);
        Task DeleteDataAsync(List<string> ids);
    }
}