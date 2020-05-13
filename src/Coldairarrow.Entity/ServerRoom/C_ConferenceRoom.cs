using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ServerRoom
{
    /// <summary>
    /// 会议室表
    /// </summary>
    [Table("C_ConferenceRoom")]
    public class C_ConferenceRoom
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 办公楼ID
        /// </summary>
        public String OfficeId { get; set; }

        /// <summary>
        /// 会议室名称
        /// </summary>
        public String ConferenceRoomName { get; set; }

        /// <summary>
        /// 会议室说明
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 预约会议地点
        /// </summary>
        public String Place { get; set; }

        /// <summary>
        /// 会议室状态
        /// </summary>
        public Int32? STATUS { get; set; }

        /// <summary>
        /// 时段
        /// </summary>
        public String TimeInterval { get; set; }

        /// <summary>
        /// 容纳人数
        /// </summary>
        public Int32? Capacity { get; set; }

        /// <summary>
        /// 会议标签属性
        /// </summary>
        public String RoomAttribute { get; set; }

        /// <summary>
        /// 会议图片
        /// </summary>
        public String RommImage { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public String CreateName { get; set; }

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