using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Dome
{
    /// <summary>
    /// 供应商联系
    /// </summary>
    [Table("D_SupplierContacts")]
    public class D_SupplierContacts
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
        public String SupplierId { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public String Contacts { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public String POSITION { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public String MobilePhone { get; set; }

        /// <summary>
        /// 座机
        /// </summary>
        public String Landline { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public String Email { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public Boolean? IsDefault { get; set; }

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
        /// 修改人
        /// </summary>
        public String UpdateName { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

    }
}