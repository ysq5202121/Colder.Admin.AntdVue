using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ServerFood
{
    /// <summary>
    /// 发布菜品
    /// </summary>
    [Table("F_PublishFood")]
    public class F_PublishFood
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
        /// 菜品Id
        /// </summary>
        public String FoodInfoId { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public Double? Price { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public String SupplierName { get; set; }

        /// <summary>
        /// 菜品名称
        /// </summary>
        public String FoodName { get; set; }

        /// <summary>
        /// 菜品描述信息
        /// </summary>
        public String FoodDesc { get; set; }

        /// <summary>
        /// 菜品数量
        /// </summary>
        public Int32? FoodQty { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public String ImgUrl { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? PublishDate { get; set; }

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