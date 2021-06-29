using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SakaryaBel.Web.Areas.Mobile.Models
{
    public class mDeadline
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string EnrollmentId { get; set; }
        public string ClassId { get; set; }
        public string CourseName { get; set; }
        public bool IsDeadline { get; set; }
        public int TotalDurationInMinutes { get; set; }
        public int? ClassCount { get; set; }
        public mActivity Activity { get; set; }
    }
}