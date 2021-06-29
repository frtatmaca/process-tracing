using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakaryaBel.Core.DomainModel.Entities
{
    public class User : BaseEntity
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }

        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string LastLoginIp { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
