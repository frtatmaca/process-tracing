using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakaryaBel.Web.ViewModel
{
    public class UserModel
    {
        public UserModel()
        {
            //termWeek = new List<TermWeekModel>();
        }

        public string UserId { get; set; }
        public string Name { get; set; }
        public string Departman { get; set; }
        public string Cheif { get; set; }
        public string ImageUrl { get; set; }
        public string Status { get; set; }   
    }
}
