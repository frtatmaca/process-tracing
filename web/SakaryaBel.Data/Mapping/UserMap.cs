using SakaryaBel.Core.DomainModel.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SakaryaBel.Data.Mapping
{
    public class UserMap : EntityTypeConfiguration<Users>
    {
        public UserMap()
        {
            ToTable("User");
            HasKey(x => x.Id);
            Property(x => x.DisplayName).HasMaxLength(100);
            Property(x => x.Title).IsOptional().HasMaxLength(100);
            Property(x => x.Description).HasMaxLength(300);
            Property(x => x.TwitterLink).HasMaxLength(300);
            Property(x => x.FacebookLink).HasMaxLength(300);
            Property(x => x.ContactTel).HasMaxLength(15);
            Property(x => x.Email).HasMaxLength(250);
            Property(x => x.LastLoginIp).HasMaxLength(20);
            Property(x => x.Password).HasMaxLength(50);
            Property(x => x.ProfileImageUrl).HasMaxLength(500);
            Property(x => x.UserName).HasMaxLength(50);

            HasMany(h => h.Roles).
            WithMany(e => e.Users).
            Map(
                m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                    m.ToTable("User_Role");
                }
            );
        }
    }
}
