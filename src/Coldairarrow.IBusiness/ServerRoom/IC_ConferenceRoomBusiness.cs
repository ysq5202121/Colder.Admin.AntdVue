using Coldairarrow.Entity.ServerRoom;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerRoom
{
    public interface IC_ConferenceRoomBusiness
    {
        Task<PageResult<C_ConferenceRoomResultDto>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<C_ConferenceRoom> GetTheDataAsync(string id);
        Task AddDataAsync(C_ConferenceRoom data);
        Task UpdateDataAsync(C_ConferenceRoom data);
        Task DeleteDataAsync(List<string> ids);
    }

    public class C_ConferenceRoomResultDto : C_ConferenceRoom
    {
        public  string OfficeName { get; set; }
    }
}