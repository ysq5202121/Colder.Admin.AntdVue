using Coldairarrow.Entity.Dome;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Dome
{
    public interface ID_SupplierContactsBusiness
    {
        Task<PageResult<D_SupplierContacts>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<D_SupplierContacts> GetTheDataAsync(string id);
        Task<List<D_SupplierContacts>> GetDataListByIdAsync(ConditionDTO input);
        Task AddDataAsync(D_SupplierContacts data);
        Task UpdateDataAsync(D_SupplierContacts data);
        Task DeleteDataAsync(List<string> ids);
    }
}