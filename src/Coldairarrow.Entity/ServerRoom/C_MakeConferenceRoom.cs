using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ServerRoom
{
    /// <summary>
    /// 预约会议室
    /// </summary>
    [Table("C_MakeConferenceRoom")]
    public class C_MakeConferenceRoom
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 会议室ID
        /// </summary>
        public String ConferenceRoomId { get; set; }

        /// <summary>
        /// 会议主题
        /// </summary>
        public String MeetingTheme { get; set; }

        /// <summary>
        /// 会议说明
        /// </summary>
        public String MeetingNotes { get; set; }

        /// <summary>
        /// 预约会议状态
        /// </summary>
        public Int32? STATUS { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public DateTime? AppointmentTime { get; set; }

        /// <summary>
        /// 会议通知
        /// </summary>
        public Boolean? IsNotice { get; set; }

        /// <summary>
        /// 会议附件
        /// </summary>
        public String Attachment { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public String CreateName { get; set; }

        /// <summary>
        /// 修改人编号
        /// </summary>
        public String UpdateId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 修改人时间
        /// </summary>
        public String UpdateName { get; set; }

    }
}