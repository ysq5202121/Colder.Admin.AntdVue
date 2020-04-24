using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerFood
{
    public interface IF_UserInfoBusiness
    {
        Task<PageResult<F_UserInfo>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<F_UserInfo> GetTheDataAsync(string id);
        Task AddDataAsync(F_UserInfo data);
        Task UpdateDataAsync(F_UserInfo data);
        Task DeleteDataAsync(List<string> ids);
    }
}