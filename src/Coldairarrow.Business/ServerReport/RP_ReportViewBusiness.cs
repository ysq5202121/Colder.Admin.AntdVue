using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Coldairarrow.Entity.ServerReport;
using Coldairarrow.IBusiness.ServerReport;
using Coldairarrow.Util;
using EFCore.Sharding;

namespace Coldairarrow.Business.ServerReport
{
    public class RP_ReportViewBusiness: BaseBusiness<RP_UserInfo>, IRP_ReportViewBusiness, ITransientDependency
    {
        public RP_ReportViewBusiness(IRepository repository)
            : base(repository)
        {

        }

        /// <summary>
        /// 获取ED订单占比
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  List<OrderProportionResultDto> GetOrderProportionListAsync(OrderReportInputDto input)
        {

            List < OrderProportionResultDto > list=new List<OrderProportionResultDto>();
            list.Add(new OrderProportionResultDto()
            {
                Item="男人",
                Count = 15
            });
            list.Add(new OrderProportionResultDto()
            {
                Item = "女人",
                Count = 30
            });
            list.Add(new OrderProportionResultDto()
            {
                Item = "小孩",
                Count = 30
            });
            return  list;
           
        }

        /// <summary>
        /// 获取ED订单的趋势统计
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public  List<OrderTrendResultDto> GetOrderTrendListAsync(OrderReportInputDto input)
        {
            List<OrderTrendResultDto> list = new List<OrderTrendResultDto>();
            list.Add(new OrderTrendResultDto()
            {
                Item = "男人",
                OrderNumber = 1,
                OrderDate = "5月1日"

            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "女人",
                OrderNumber = 2,
                OrderDate = "5月1日"
            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "小孩",
                OrderNumber = 3,
                OrderDate = "5月1日"
            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "男人",
                OrderNumber = 3,
                OrderDate = "5月2日"

            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "女人",
                OrderNumber = 2,
                OrderDate = "5月2日"
            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "小孩",
                OrderNumber = 1,
                OrderDate = "5月2日"
            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "男人",
                OrderNumber = 2,
                OrderDate = "5月3日"

            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "女人",
                OrderNumber = 1,
                OrderDate = "5月3日"
            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "小孩",
                OrderNumber = 3,
                OrderDate = "5月3日"
            });
            return list;
        }

        /// <summary>
        /// 组合数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Object GetOrderDetailListAsync(OrderReportInputDto input)
        {
            List<OrderTrendResultDto> list = new List<OrderTrendResultDto>();
            list.Add(new OrderTrendResultDto()
            {
                Item = "男人",
                OrderNumber = 1,
                OrderDate = "5月1日"

            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "女人",
                OrderNumber = 2,
                OrderDate = "5月1日"
            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "小孩",
                OrderNumber = 3,
                OrderDate = "5月1日"
            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "男人",
                OrderNumber = 3,
                OrderDate = "5月2日"

            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "女人",
                OrderNumber = 2,
                OrderDate = "5月2日"
            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "小孩",
                OrderNumber = 1,
                OrderDate = "5月2日"
            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "男人",
                OrderNumber = 2,
                OrderDate = "5月3日"

            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "女人",
                OrderNumber = 1,
                OrderDate = "5月3日"
            });
            list.Add(new OrderTrendResultDto()
            {
                Item = "小孩",
                OrderNumber = 3,
                OrderDate = "5月3日"
            });
            DataTable dt =new DataTable();
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Item", typeof(string));
            list.GroupBy(a=>a.OrderDate).ForEach(a =>
            {
                dt.Columns.Add(a.Key, typeof(string));
            });
            dt.Columns.Add("汇总", typeof(int));
            list.ForEach(a =>
            { 
               //添加一行数据
               var rows = dt.AsEnumerable().Where(b => b["Item"].ToString() == a.Item)?.FirstOrDefault();
               if (rows==null)
               {
                   var newRow= dt.NewRow();
                   newRow["Item"] = a.Item;
                   newRow["Id"] = dt.Rows.Count + 1;
                   foreach (DataColumn dtColumn in dt.Columns)
                   {
                       if (dt.Columns.Contains(a.OrderDate))
                       {
                           newRow[a.OrderDate] = a.OrderNumber;
                       }
                   }
                   dt.Rows.Add(newRow);
               }
               else
               {
                   foreach (DataColumn dtColumn in dt.Columns)
                   {
                       if (dt.Columns.Contains(a.OrderDate))
                       {
                           rows[a.OrderDate] = a.OrderNumber;
                       }
                   }
               }
            });
            //行汇总
            foreach (DataRow dtRow in dt.Rows)
            {
                var sumCount = 0;
                list.GroupBy(a=>a.OrderDate).Select(a=>a.Key).ForEach(a =>
                {
                    if (dt.Columns.Contains(a))
                    {
                        sumCount += Convert.ToInt32(dtRow[a]);
                    }
                });
                dtRow["汇总"] = sumCount;
            }
            var lastRow = dt.NewRow();
            lastRow["Item"] = "汇总";
            lastRow["Id"] = dt.Rows.Count + 1;
            //列汇总
            list.Select(a => a.OrderDate).ForEach(a =>
            {
                var sumCount = 0;
                dt.AsEnumerable().Select(b => b[a]).ForEach(c => { sumCount += Convert.ToInt32(c); });
                lastRow[a] = sumCount;
            });
            //汇总列行总
            var lastSumCount = 0;
            dt.AsEnumerable().Select(b => b["汇总"]).ForEach(c => { lastSumCount += Convert.ToInt32(c); });
            lastRow["汇总"] = lastSumCount;
            dt.Rows.Add(lastRow);
            return dt;
        }
    }
}
