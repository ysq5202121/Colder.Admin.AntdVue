﻿using Coldairarrow.Entity.ServerFood;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ServerFood
{
    public interface IF_OrderBusiness
    {
        Task<PageResult<IF_OrderResultDTO>> GetDataListAsync(PageInput<ConditionDTO> input);

        Task PlaceOrderAsync(List<IF_OrderInputDTO> data);
        Task<F_Order> GetTheDataAsync(string id);
        Task AddDataAsync(F_Order data);
        Task UpdateDataAsync(F_Order data);
        Task DeleteDataAsync(List<string> ids);
    }

    public class IF_OrderInputDTO
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public int Num { get; set; }
        public string UserID { get; set; }
    }

    public class IF_OrderResultDTO: F_Order
    {
        public string UserName { get; set; }

    }
}