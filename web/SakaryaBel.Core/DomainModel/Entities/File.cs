using SakaryaBel.Core.Enums;
using SakaryaBel.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakaryaBel.Core.DomainModel.Entities
{
    public class File : IAuditable
    {
        public File()
        {
            this.FileVersion = 1;
            this.OwnerType = FileOwnerType.None;
            this.IsDirectory = false;

            this.Activity = new HashSet<Activity>();
        }

        public File(FileOwnerType ownerType, int ownerId, FileType fileType)
            : this()
        {
            this.OwnerId = ownerId;
            this.OwnerType = ownerType;
            this.FileType = fileType;
            this.Activity = new HashSet<Activity>();
        }

        public int FileId { get; set; }
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public string ContentType { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public ActiveStatus ActiveStatus { get; set; }


        public string ThumbnailName { get; set; }

        /// <summary>
        /// File extension is stored without dot like pdf or docx.
        /// </summary>
        public string Extension { get; set; }

        public string Description { get; set; }
        public string Category { get; set; }

        /// <summary>
        /// Older versions may be stored in the file repository. This is an incrementing number like 1,2,3...
        /// </summary>
        public int FileVersion { get; set; }

        /// <summary>
        /// File size in bytes
        /// </summary>
        public long? Size { get; set; }

        /// <summary>
        /// This entry may denote a directory.
        /// </summary>
        public bool IsDirectory { get; set; }

        public virtual ICollection<Activity> Activity { get; set; }

        #region relation definitions
        public FileOwnerType OwnerType { get; set; }

        public int OwnerId { get; set; }

        public int? UserId { get; set; }
        #endregion

        #region Audit
        public int? CreatedByUserId { get; set; } public int? SimulatedByUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? LastUpdatedByUserId { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public Guid TrackingGuid { get; set; }
        #endregion
    }
}
