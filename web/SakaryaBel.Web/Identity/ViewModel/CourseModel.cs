using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakaryaBel.Web.ViewModel
{
    public class CourseModel
    {
        public CourseModel()
        {
            //termWeek = new List<TermWeekModel>();
        }

        public int ActivityId { get; set; }
        public string Name { get; set; }

        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }

        public string Description { get; set; }
        public string Abbreviation { get; set; }
        public string TaskDetail { get; set; }

        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string FileEmbed { get; set; }
        
    }
}
