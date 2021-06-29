using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SakaryaBel.Web.Areas.Mobile.Models
{
    public class mAnnouncement
    {
        public mAnnouncement()
        {
            this.ShowOnStartup = false;
        }
        public string AnnouncementId { get; set; }
        public string ClassId { get; set; }
        public string CourseId { get; set; }
        public int ImportanceScore { get; set; }
        public string FromUserId { get; set; }
        public string FromDisplayName { get; set; }
        public string[] Targets { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        //public MessageState SenderState { get; set; }
        public bool ShowOnStartup { get; set; }
        public bool HasEmail { get; set; }
        public bool HasSms { get; set; }
        public bool HasAttachment { get; set; }
        public string SendDate { get; set; }
        public bool Unread { get; set; }
    }
}