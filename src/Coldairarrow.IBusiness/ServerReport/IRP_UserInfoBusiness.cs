using Coldairarrow.Entity.ServerReport;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerReport
{
    public interface IRP_UserInfoBusiness
    {
        Task<PageResult<RP_UserInfo>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<RP_UserInfo> GetTheDataAsync(string id);
        Task AddDataAsync(RP_UserInfo data);
        Task UpdateDataAsync(RP_UserInfo data);
        Task DeleteDataAsync(List<string> ids);
        Task<string> Login(string code);
    }
}