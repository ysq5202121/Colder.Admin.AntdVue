using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Base_Manage
{
    /// <summary>
    /// 字典表
    /// </summary>
    [Table("Base_Dictionary")]
    public class Base_Dictionary
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        public String DicKey { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public String DicValue { get; set; }

        /// <summary>
        /// 显示值
        /// </summary>
        public String DicDisplayValue { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public String DicDesc { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public EnumDicStatus STATUS { get; set; }

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

    /// <summary>
    /// 字典状态
    /// </summary>
    public enum EnumDicStatus
    {
        /// <summary>
        /// 系统
        /// </summary>
        [Description("系统")]
        System=1,
        /// <summary>
        /// 表状态
        /// </summary>
        [Description("表状态")]
        TableStatus =2,
        /// <summary>
        /// 配置
        /// </summary>
        [Description("配置")]
        Config =3
    }
}