using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_DepartmentRelationBusiness
    {
        Task<PageResult<Base_DepartmentRelation>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<Base_DepartmentRelation> GetTheDataAsync(string id);
        Task<List<string>> GetNewDepartmentListAsync();
        Task AddDataAsync(Base_DepartmentRelation data);
        Task UpdateDataAsync(Base_DepartmentRelation data);
        Task DeleteDataAsync(List<string> ids);
    }
}