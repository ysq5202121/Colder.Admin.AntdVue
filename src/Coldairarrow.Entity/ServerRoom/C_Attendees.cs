using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ServerRoom
{
    /// <summary>
    /// 预会人员
    /// </summary>
    [Table("C_Attendees")]
    public class C_Attendees
    {

        /// <summary>
        /// 编号
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 会议ID
        /// </summary>
        public String MakeConferenceRoomId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public String UserWeChatID { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public String UserWeChatName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public String UserWeChatImg { get; set; }

        /// <summary>
        /// 用户部门信息
        /// </summary>
        public String UserWeChatDepartment { get; set; }

        /// <summary>
        /// 与会人状态
        /// </summary>
        public Int32? STATUS { get; set; }

        /// <summary>
        /// 与会人员类型
        /// </summary>
        public Int32? AttendeesType { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public String CreateName { get; set; }

        /// <summary>
        /// 创建时间
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