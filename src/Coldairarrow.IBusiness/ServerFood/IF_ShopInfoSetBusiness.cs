using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerFood
{
    public interface IF_ShopInfoSetBusiness
    {
        Task<PageResult<IF_ShopInfoSetResultDTO>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<F_ShopInfoSet> GetTheDataAsync(string id);
        Task AddDataAsync(F_ShopInfoSet data);
        Task UpdateDataAsync(F_ShopInfoSet data);
        Task DeleteDataAsync(List<string> ids);
    }

    public class IF_ShopInfoSetResultDTO : F_ShopInfoSet
    {
        public string ShopName { get; set; }

    }

}