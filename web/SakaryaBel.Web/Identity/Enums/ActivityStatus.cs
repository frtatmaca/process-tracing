using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakaryaBel.Web.Enums
{
    public enum ActivityStatus : int
    {
        New = 0,
        AssignedSuperCheif = 1,
        Complete = 2,
        Inspected = 3,
        CloseManager = 4
    }
}
