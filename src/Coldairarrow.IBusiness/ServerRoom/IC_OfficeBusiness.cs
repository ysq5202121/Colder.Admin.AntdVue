using Coldairarrow.Entity.ServerRoom;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerRoom
{
    public interface IC_OfficeBusiness
    {
        Task<PageResult<C_Office>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<List<C_Office>> GetDataListAllAsync();
        Task<C_Office> GetTheDataAsync(string id);
        Task AddDataAsync(C_Office data);
        Task UpdateDataAsync(C_Office data);
        Task DeleteDataAsync(List<string> ids);

    }
}