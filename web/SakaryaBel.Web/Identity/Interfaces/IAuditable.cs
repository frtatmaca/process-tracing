using System;

namespace SakaryaBel.Web.Interfaces
{
    public interface IAuditable
    {
        DateTime? CreatedDate { get; set; }
        string CreatedByUserId { get; set; }
        DateTime? LastUpdatedDate { get; set; }
        string LastUpdatedByUserId { get; set; }
        string SimulatedByUserId { get; set; }
        Guid TrackingGuid { get; set; }

    }
}
