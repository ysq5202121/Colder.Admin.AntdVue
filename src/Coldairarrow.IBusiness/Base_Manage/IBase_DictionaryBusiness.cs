using System;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_DictionaryBusiness
    {
        Task<PageResult<Base_DictionaryResultDto>> GetDataListAsync(PageInput<ConditionDTO> input);
        Task<Base_Dictionary> GetTheDataAsync(string id);
        Task AddDataAsync(Base_Dictionary data);
        Task UpdateDataAsync(Base_Dictionary data);
        Task DeleteDataAsync(List<string> ids);
    }

    public class Base_DictionaryResultDto : Base_Dictionary
    {
        public Base_DictionaryResultDto()
        {

        }
        public Base_DictionaryResultDto(Base_Dictionary b)
        {
    
        }
        public string StatusName
        {
            get { return STATUS.GetDescription(); }
        }
    }
}