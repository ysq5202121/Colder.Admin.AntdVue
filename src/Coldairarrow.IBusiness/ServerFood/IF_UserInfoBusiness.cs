using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerFood
{
    public interface IF_UserInfoBusiness
    {
        Task<PageResult<IF_UserInfoResultDto>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<F_UserInfo> GetTheDataAsync(string id);
        Task<F_UserInfo> GetTheDataByWeChatIdAsync(string id);
        Task<IF_UserInfoResultDto> GetUserInfoToMoblieAsync();
        Task<string> Login(string code);
        Task<bool> CheckLogin(string code);

        Task AddDataAsync(F_UserInfo data);
        Task UpdateShopNameAsync(F_UserInfo data);
        Task UpdateDataAsync(F_UserInfo data);
        Task DeleteDataAsync(List<string> ids);
        Task TimedRefreshDepartment();

    }

    public class IF_UserInfoResultDto : F_UserInfo
    {
        public string ShopName { get; set; }

    }
}