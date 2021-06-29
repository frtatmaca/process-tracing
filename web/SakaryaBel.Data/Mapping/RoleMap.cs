using SakaryaBel.Core.DomainModel.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SakaryaBel.Data.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            ToTable("Role");
            HasKey(x => x.Id);
            Property(x => x.RoleName).HasMaxLength(100);
        }
    }
}
