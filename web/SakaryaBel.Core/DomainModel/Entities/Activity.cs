using SakaryaBel.Core.Enums;
using SakaryaBel.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SakaryaBel.Core.DomainModel.Entities
{
    public class Activity : IAuditable
    {
        public Activity()
        {
            this.ActiveStatus = ActiveStatus.Active;
            this.TrackingGuid = Guid.NewGuid();
        }

        public int ActivityId { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        public string Description { get; set; }
        public string Abbreviation { get; set; }

        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? TaskDeadLine { get; set; }

        public string FileEmbed { get; set; }
        public string Location { get; set; }

        [AllowHtml]
        public string TaskDetail { get; set; }
        public bool IsCommon { get; set; }

        public ActiveStatus ActiveStatus { get; set; }
        public ActivityStatus ActivityStatus { get; set; }
        public ActivityType ActivityType { get; set; }
        public int? ActionId { get; set; }

       
        // Custom fields
        public string CustomProperty1 { get; set; }
        public string CustomProperty2 { get; set; }
        public string CustomProperty3 { get; set; }
        public string CustomProperty4 { get; set; }
        public string CustomProperty5 { get; set; }
        public string ExternalKey { get; set; }

        public int? FileId { get; set; }
        public virtual File File { get; set; }

        #region Audit
        public int? CreatedByUserId { get; set; }
        public int? SimulatedByUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedByUserId { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public Guid TrackingGuid { get; set; }
        #endregion

    }
}
