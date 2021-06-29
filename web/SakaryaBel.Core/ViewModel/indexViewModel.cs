using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakaryaBel.Core.ViewModel
{
    public class indexViewModel
    {
        public indexViewModel() {            
            course = new List<CourseModel>();
        }

        public List<CourseModel> course { get; set; }
    }
}
