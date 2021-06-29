using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SakaryaBel.Web.Areas.Mobile.Models
{
    public class mEnrollment
    {
        public string EnrollmentId { get; set; }
        public string ClassId { get; set; }
        public string ActivityId { get; set; }
        public double? Score { get; set; }
        public string Status { get; set; }
        public int Progress { get; set; }
        public int? TotalSeconds { get; set; }
        public DateTime? FirstAttemptDate { get; set; }
        public DateTime? LastAttemptDate { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}