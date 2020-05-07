using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerFood
{
    public interface IF_ShopInfoBusiness
    {
        Task<PageResult<F_ShopInfo>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<List<F_ShopInfo>> GetDataListToMoblieAsync();

        Task<List<F_ShopInfo>> GetDataListAllAsync();
        Task<F_ShopInfo> GetTheDataAsync(string id);
        Task AddDataAsync(F_ShopInfo data);
        Task UpdateDataAsync(F_ShopInfo data);
        Task DeleteDataAsync(List<string> ids);
    }
}