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
    public class Message : IAuditable
    {
        public Message()
        {
            MessajeInfo = MessageProcessInfo.CanNotRead;
            ActiveStatus = ActiveStatus.Active;
            TrackingGuid = Guid.NewGuid();
        }
        public int MessageId { get; set; }

        public string SenderUserName { get; set; }
        public string SenderEmail { get; set; }
        [AllowHtml]
        public string SenderWebsite { get; set; }
        public string Subject { get; set; }
        [AllowHtml]
        public string Body { get; set; }

        public int? answeringUserId { get; set; }
        public int? answeringMesajId { get; set; }

        public ActiveStatus ActiveStatus { get; set; }
        public MessageProcessInfo MessajeInfo { get; set; }

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
