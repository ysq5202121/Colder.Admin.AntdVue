using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ServerFood
{
    /// <summary>
    /// 订单信息
    /// </summary>
    [Table("F_Order")]
    public class F_Order
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public String OrderCode { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public String UserInfoId { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public Int32? OrderCount { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// 取餐编码
        /// </summary>
        public string TakeFoodCode { get; set; }

        /// <summary>
        /// 取餐人
        /// </summary>
        public string TakeFoodName { get; set; }

        /// <summary>
        /// 创建人编号 当前用户ID
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public String CreatorName { get; set; }

        /// <summary>
        /// 创建日期 默认为当前时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改人编号
        /// </summary>
        public String UpdateId { get; set; }

        /// <summary>
        /// 修改人时间
        /// </summary>
        public String UpdateName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 可取消时间
        /// </summary>
        public DateTime? CancellableTime { get; set; }
        /// <summary>
        /// 可评价时间
        /// </summary>
        public DateTime? CanEvaluableTime { get; set; }
        /// <summary>
        /// 可领取时间
        /// </summary>
        public DateTime? StartReceiveTime { get; set; }

    }
}