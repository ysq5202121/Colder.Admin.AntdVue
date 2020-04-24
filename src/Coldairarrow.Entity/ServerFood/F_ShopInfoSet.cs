using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ServerFood
{
    /// <summary>
    /// 门店设置
    /// </summary>
    [Table("F_ShopInfoSet")]
    public class F_ShopInfoSet
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        public String ShopInfoId { get; set; }

        /// <summary>
        /// 点餐开始时间
        /// </summary>
        public DateTime? OrderBeginDate { get; set; }

        /// <summary>
        /// 点餐结束时间
        /// </summary>
        public DateTime? OrderBeginEnd { get; set; }

        /// <summary>
        /// 开始点餐提醒信息
        /// </summary>
        public String OrderBeginRemind { get; set; }

        /// <summary>
        /// 结束点餐提醒信息
        /// </summary>
        public String OrderEndRemind { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public String CreatorName { get; set; }

        /// <summary>
        /// 创建日期 默认为当前时间
        /// </summary>
        public DateTime? CreateDate { get; set; }

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
        public DateTime? UpdateDate { get; set; }

    }
}