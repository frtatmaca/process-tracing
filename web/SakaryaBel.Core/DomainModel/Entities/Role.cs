using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SakaryaBel.Core.DomainModel.Entities
{
    public class Role : BaseEntity
    {
        public Role()
        {
            Users = new HashSet<Users>();
        }

        public string RoleName { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
