using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerFood
{
    public interface IF_OrderInfoBusiness
    {
        Task<PageResult<F_OrderInfo>> GetDataListAsync(PageInput<ConditionDTO> input);

        Task<List<IF_OrderInfoResultDto>> GetDataListNoPageAsync(ConditionDTO input);

        Task<List<IF_OrderInfoResultDto>> GetDataListToMoblieAsync(ConditionDTO input);

        Task<F_OrderInfo> GetTheDataAsync(string id);
        Task AddDataAsync(F_OrderInfo data);
        Task UpdateDataAsync(F_OrderInfo data);
        Task DeleteDataAsync(List<string> ids);
    }

    public class IF_OrderInfoResultDto: F_OrderInfo
    {
        public string FoodName { get; set; }
    }
}