using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coldairarrow.IBusiness.ServerReport;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;

namespace Coldairarrow.Api.Controllers.ServerReport
{
    [Route("/ServerReport/[controller]/[action]")]
    public class RP_ReportViewController : BaseApiController
    {
        public RP_ReportViewController(IRP_ReportViewBusiness rpReportViewBusiness)
        {
            _rpReportViewBusiness = rpReportViewBusiness;
        }

        IRP_ReportViewBusiness _rpReportViewBusiness { get; }

        [HttpPost]
        [NoCheckJWT]
        [CheckJWTClient(AppId = (int)EnumWeChatAppType.Report)]
        public  List<OrderProportionResultDto> GetOrderProportionList(OrderReportInputDto input)
        {
            return  _rpReportViewBusiness.GetOrderProportionListAsync(input);
        }

        [HttpPost]
        [NoCheckJWT]
        [CheckJWTClient(AppId = (int)EnumWeChatAppType.Report)]
        public List<OrderTrendResultDto> GetOrderTrendList(OrderReportInputDto input)
        {
            return _rpReportViewBusiness.GetOrderTrendListAsync(input);
        }

        [HttpPost]
        [NoCheckJWT]
        [CheckJWTClient(AppId = (int)EnumWeChatAppType.Report)]
        public Object GetOrderDetailList(OrderReportInputDto input)
        {
            return _rpReportViewBusiness.GetOrderDetailListAsync(input);
        }

    }
}
