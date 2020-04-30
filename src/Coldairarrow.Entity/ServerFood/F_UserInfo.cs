using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ServerFood
{
    /// <summary>
    /// 用户信息管理
    /// </summary>
    [Table("F_UserInfo")]
    public class F_UserInfo
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public Boolean? IsAdmin { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public String ShopInfoId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public String Department { get; set; }

        /// <summary>
        /// 微信用户ID
        /// </summary>
        public String WeCharUserId { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public String UserImgUrl { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public String CreatorName { get; set; }

        /// <summary>
        /// 创建日期 
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

    }
}