using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerFood
{
    public interface IF_ShopInfoSetBusiness
    {
        Task<PageResult<F_ShopInfoSet>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<F_ShopInfoSet> GetTheDataAsync(string id);
        Task AddDataAsync(F_ShopInfoSet data);
        Task UpdateDataAsync(F_ShopInfoSet data);
        Task DeleteDataAsync(List<string> ids);
    }
}