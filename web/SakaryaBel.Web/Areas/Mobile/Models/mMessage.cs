using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SakaryaBel.Web.Areas.Mobile.Models
{
    public class mMessage
    {
        public string MessageId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public string SenderUserId { get; set; }
        public string SenderFullName { get; set; }
        public string TargetUserId { get; set; }
        public string TargetFullName { get; set; }
        public bool IsNew { get; set; }

    }
}