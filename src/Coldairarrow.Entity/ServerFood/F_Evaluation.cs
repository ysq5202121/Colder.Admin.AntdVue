﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ServerFood
{
    /// <summary>
    /// 菜品评价
    /// </summary>
    [Table("F_Evaluation")]
    public class F_Evaluation
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public String UserInfoId { get; set; }

        /// <summary>
        /// 菜品ID
        /// </summary>
        public String FoodInfoId { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public Int32? Score { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public String FoodContent { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public String Remark { get; set; }

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

    }
}