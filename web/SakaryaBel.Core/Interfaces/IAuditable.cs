using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakaryaBel.Core.Interfaces
{
    public interface IAuditable
    {
        DateTime? CreatedDate { get; set; }
        int? CreatedByUserId { get; set; }
        DateTime? LastUpdatedDate { get; set; }
        int? LastUpdatedByUserId { get; set; }
        int? SimulatedByUserId { get; set; }
        Guid TrackingGuid { get; set; }

    }
}
