using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerFood
{
    public interface IF_FoodInfoBusiness
    {
        Task<PageResult<F_FoodInfo>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<F_FoodInfo> GetTheDataAsync(string id);
        Task AddDataAsync(F_FoodInfo data);
        Task UpdateDataAsync(F_FoodInfo data);
        Task DeleteDataAsync(List<string> ids);
    }
}