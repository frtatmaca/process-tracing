using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakaryaBel.Core.DomainModel.Entities
{
    public class Users : BaseEntity
    {
        public Users()
        {
            Roles = new HashSet<Role>();
        }

        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TwitterLink { get; set; }
        public string FacebookLink { get; set; }
        public string ContactTel { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string LastLoginIp { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}
