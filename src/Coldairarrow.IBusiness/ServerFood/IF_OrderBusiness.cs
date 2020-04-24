using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerFood
{
    public interface IF_OrderBusiness
    {
        Task<PageResult<F_Order>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<F_Order> GetTheDataAsync(string id);
        Task AddDataAsync(F_Order data);
        Task UpdateDataAsync(F_Order data);
        Task DeleteDataAsync(List<string> ids);
    }
}