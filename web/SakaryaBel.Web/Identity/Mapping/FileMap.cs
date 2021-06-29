using SakaryaBel.Web.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakaryaBel.Web.Mapping
{
    public class FileMap : EntityTypeConfiguration<File>
    {
        public FileMap()
        {
            HasKey(model => model.FileId);

            Property(model => model.FileName)
                            .HasMaxLength(250)
                            .IsRequired()
                            .IsUnicode();

            Property(model => model.Category)
                            .HasMaxLength(250)
                            .IsOptional()
                            .IsUnicode();

            Property(model => model.Description)
                            .HasMaxLength(4000)
                            .IsOptional()
                            .IsUnicode();

            Property(model => model.Extension)
                            .HasMaxLength(10)
                            .IsOptional()
                            .IsUnicode();

            Property(model => model.ThumbnailName)
                            .HasMaxLength(250)
                            .IsUnicode();

            Property(model => model.ActiveStatus)
              .IsRequired();


            Property(model => model.ContentType)
                            .HasMaxLength(100)
                            .IsRequired()
                            .IsUnicode();

            Property(model => model.Height)
                            .IsOptional();

            Property(model => model.Width)
                            .IsOptional();

            Property(model => model.Size)
                           .IsOptional();

            Property(model => model.FileVersion)
                          .IsRequired();

            Property(model => model.OwnerType)
                          .IsRequired();

            Property(model => model.IsDirectory)
                           .IsRequired();

            Property(model => model.OwnerId)
                .IsRequired();

            Property(model => model.CreatedDate)
                      .IsOptional()
                      ;

            Property(model => model.LastUpdatedDate)
                .IsOptional()
                ;
        }
    }
}
