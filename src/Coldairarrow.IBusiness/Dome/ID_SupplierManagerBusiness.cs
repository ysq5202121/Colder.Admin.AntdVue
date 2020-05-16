using Coldairarrow.Entity.Dome;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Dome
{
    public interface ID_SupplierManagerBusiness
    {
        Task<PageResult<D_SupplierManager>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<D_SupplierManager> GetTheDataAsync(string id);
        Task<SupplierManagerStatusDto> GetStatusListAsync();
        Task AddDataAsync(D_SupplierManager data);
        Task UpdateDataAsync(D_SupplierManager data);
        Task DeleteDataAsync(List<string> ids);
    }

    public class SupplierManagerStatusDto
    {
        public List<SelectOption> SupplierType { get; set; }

        public List<SelectOption> Region { get; set; }

        public List<SelectOption> City { get; set; }
    }
}