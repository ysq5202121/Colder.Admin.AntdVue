using Coldairarrow.Entity.ServerRoom;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerRoom
{
    public interface IC_AttendeesBusiness
    {
        Task<PageResult<C_Attendees>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<C_Attendees> GetTheDataAsync(string id);
        Task AddDataAsync(C_Attendees data);
        Task UpdateDataAsync(C_Attendees data);
        Task DeleteDataAsync(List<string> ids);
    }
}