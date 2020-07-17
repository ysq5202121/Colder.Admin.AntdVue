using System;
using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerFood
{
    public interface IF_FoodInfoBusiness
    {
        Task<PageResult<F_FoodInfoResultDto>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<F_FoodInfo> GetTheDataAsync(string id);
        Task PublishFoodDataAsync(List<string> ids, DateTime? dt);
        Task AddDataAsync(F_FoodInfo data);
        Task UpdateDataAsync(F_FoodInfo data);
        Task DeleteDataAsync(List<string> ids);
    }

    public class F_FoodInfoResultDto : F_FoodInfo
    {
        public  string ShopName { get; set; }
    }
}