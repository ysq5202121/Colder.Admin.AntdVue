using System;
using System.Collections.Generic;
using System.Text;

namespace Coldairarrow.IBusiness.ServerReport
{
    public interface IRP_ReportViewBusiness
    {
        List<OrderProportionResultDto> GetOrderProportionListAsync(OrderReportInputDto input);
        List<OrderTrendResultDto> GetOrderTrendListAsync(OrderReportInputDto input);
        Object GetOrderDetailListAsync(OrderReportInputDto input);
    }

    public class OrderProportionResultDto
    {
        public string Item { get; set; }
        public int Count { get; set; }
    }

    public class OrderReportInputDto
    {
        public string SearchType { get; set; }
        public int Day { get; set; }
    }

    public class OrderTrendResultDto
    {
        public string Item { get; set; }
        public string OrderDate { get; set; }
        public int OrderNumber { get; set; }
    }
}
