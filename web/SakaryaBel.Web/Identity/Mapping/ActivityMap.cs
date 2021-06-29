using SakaryaBel.Web.DomainModel.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SakaryaBel.Web.Mapping
{
    public class ActivityMap : EntityTypeConfiguration<Activity>
    {
        public ActivityMap()
        {
            ToTable("Activity");

            HasKey(model => model.ActivityId);

            Property(model => model.Name)
                .HasMaxLength(250)
                .IsRequired()
                .IsUnicode();

            Property(model => model.Description)
                .HasMaxLength(8000)
                .IsRequired()
                .IsUnicode();            

            Property(model => model.Abbreviation)
                .HasMaxLength(300)
                .IsRequired()
                .IsUnicode();           

            Property(model => model.FileEmbed)
               .HasMaxLength(4000)
               .IsOptional()
               .IsUnicode();

            Property(model => model.TaskDetail)
               .HasMaxLength(8000)
               .IsOptional()
               .IsUnicode();

            Property(model => model.TaskDeadLine)
               .IsOptional();

            Property(model => model.ActiveStatus)
               .IsRequired();

          
            Property(model => model.CreatedDate)
                .IsOptional();

            Property(model => model.LastUpdatedDate)
                .IsOptional();

            Property(model => model.ExternalKey)
                .IsOptional()
                .HasMaxLength(250)
                .IsUnicode();            
        }
    }
}
