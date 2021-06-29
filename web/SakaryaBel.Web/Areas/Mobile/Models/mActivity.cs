using SakaryaBel.Web.Areas.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SakaryaBel.Web.Areas.Mobile.Models
{
    public class mActivity
    {
        public mActivity()
        {
            this.Files = new List<mFile>();
            this.Weeks = new List<int>();
            this.Units = new List<string>();
        }
        public string ActivityId { get; set; }
        public string CourseId { get; set; }
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ActivityBeginDate { get; set; }
        public DateTime? ActivityEndDate { get; set; }
        public List<string> Units { get; set; }
        public List<int> Weeks { get; set; }
        public bool AlwaysOnTop { get; set; }
        public string Tumbnail { get; set; }
        public string LaunchUrl { get; set; }
        
        public List<mFile> Files { get; set; }
    }
}