using Coldairarrow.Entity.ServerRoom;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerRoom
{
    public interface IC_ConferenceRoomBusiness
    {
        Task<PageResult<C_ConferenceRoom>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<C_ConferenceRoom> GetTheDataAsync(string id);
        Task AddDataAsync(C_ConferenceRoom data);
        Task UpdateDataAsync(C_ConferenceRoom data);
        Task DeleteDataAsync(List<string> ids);
    }
}