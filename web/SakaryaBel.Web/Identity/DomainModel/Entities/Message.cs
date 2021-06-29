using SakaryaBel.Web.Enums;
using SakaryaBel.Web.Identity;
using System;
using System.Web.Mvc;

namespace SakaryaBel.Web.DomainModel.Entities
{
    public class Message
    {
        public Message()
        {
            MessajeInfo = MessageProcessInfo.CanNotRead;
            ActiveStatus = ActiveStatus.Active;
            TrackingGuid = Guid.NewGuid();
        }
        public int MessageId { get; set; }
        [AllowHtml]
        public string Body { get; set; }

        public ActiveStatus ActiveStatus { get; set; }
        public MessageProcessInfo MessajeInfo { get; set; }

        //UserId
        public string Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int? ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

        [AllowHtml]
        public string SenderWebsite { get; set; }
        public string SenderUserName { get; set; }
        public string SenderEmail { get; set; }

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
