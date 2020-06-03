using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Coldairarrow.Business.ServerFood
{
    public interface IF_PublishFoodBusiness
    {
        Task<PageResult<F_PublishFoodResultDto>> GetDataListAsync(PageInput<F_PublishFoodInputDto> input);
        Task<List<F_PublishFoodResultDto>> GetDataListToMoblieAsync(ConditionDTO input);
        Task<F_PublishFood> GetTheDataAsync(string id);
        Task AddDataAsync(F_PublishFood data);
        Task UpdateDataAsync(F_PublishFood data);
        Task DeleteDataAsync(List<string> ids);
    }

    public class F_PublishFoodResultDto : F_PublishFood
    {
        public int? Sorce { get; set; }

        public string ShopName { get; set; }
        public DateTime? BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

    }

    public class F_PublishFoodInputDto
    {
        public string Condition { get; set; }
        public string Keyword { get; set; }

        public string PublishFoodTime { get; set; }
    }
}