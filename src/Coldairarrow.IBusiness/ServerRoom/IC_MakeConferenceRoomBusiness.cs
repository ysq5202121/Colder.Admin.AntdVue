using Coldairarrow.Entity.ServerRoom;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerRoom
{
    public interface IC_MakeConferenceRoomBusiness
    {
        Task<PageResult<C_MakeConferenceRoom>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<C_MakeConferenceRoom> GetTheDataAsync(string id);
        Task AddDataAsync(C_MakeConferenceRoom data);
        Task UpdateDataAsync(C_MakeConferenceRoom data);
        Task DeleteDataAsync(List<string> ids);
    }
}