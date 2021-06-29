using Microsoft.AspNet.Identity.EntityFramework;
using SakaryaBel.Web.DomainModel.Entities;
using SakaryaBel.Web.Enums;
using System;

namespace SakaryaBel.Web.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.TrackingGuid = Guid.NewGuid();
            this.ActiveStatus = ActiveStatus.Active;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public ActiveStatus ActiveStatus { get; set; }
        public UserType UserType { get; set; }

        public int? FileId { get; set; }
        public virtual File File { get; set; }

        public virtual ApplicationUser Cheif { get; set; }

        #region Audit

        public virtual ApplicationUser CreatedByUser { get; set; }
        public string SimulatedByUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual ApplicationUser LastUpdatedByUser { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public Guid TrackingGuid { get; set; }
        #endregion
    }
}