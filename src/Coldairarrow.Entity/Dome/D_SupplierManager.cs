using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Dome
{
    /// <summary>
    /// 供应商管理
    /// </summary>
    [Table("D_SupplierManager")]
    public class D_SupplierManager
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 供应商全称
        /// </summary>
        public String SupplierName { get; set; }

        /// <summary>
        /// 供应商英文名称
        /// </summary>
        public String SupplierEnName { get; set; }

        /// <summary>
        /// 供应商地址
        /// </summary>
        public String SupplierAddress { get; set; }

        /// <summary>
        /// 供应商代码
        /// </summary>
        public String SupplierCode { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        public String Region { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public String City { get; set; }

        /// <summary>
        /// 供应商状态
        /// </summary>
        public Int32? STATUS { get; set; }

        /// <summary>
        /// 供应商类型
        /// </summary>
        public Int32? SupplierType { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public String CreatorName { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// 修改人编号
        /// </summary>
        public String UpdateId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public String UpdateName { get; set; }

    }
}