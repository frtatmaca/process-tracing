using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SakaryaBel.Web.Areas.Mobile.Models
{
    public class mCourse
    {
        public mCourse()
        {
            this.Activities = new List<mActivity>();
            this.Deadlines = new List<mDeadline>();
            this.Teachers = new List<mUser>();
            this.DisplayUnits = new List<string>();
            this.DisplayWeeks = new List<int>();
        }
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string ProgramId { get; set; }
        public string ProgramName { get; set; }
        public string OrganizationId { get; set; }
        public string TermId { get; set; }
        public mEnrollment Enrollment { get; set; }
        public List<mActivity> Activities { get; set; }
        public List<mDeadline> Deadlines { get; set; }
        public List<mUser> Teachers { get; set; }
        public List<int> DisplayWeeks { get; set; }
        public List<string> DisplayUnits { get; set; }
    }
}