using SakaryaBel.Web.DomainModel.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SakaryaBel.Web.Mapping
{
    public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            ToTable("Message");
            HasKey(model => model.MessageId);

            Property(model => model.SenderUserName)
                .HasMaxLength(250)
                .IsRequired()
                .IsUnicode();

            Property(model => model.SenderEmail)
                .HasMaxLength(250)
                .IsRequired()
                .IsUnicode();

            Property(model => model.Body)
                .IsRequired()
                .IsUnicode();

            Property(model => model.SenderWebsite)
                .IsOptional();

            Property(model => model.ActiveStatus)
               .IsRequired();

            Property(model => model.MessajeInfo)
               .IsRequired();

            Property(model => model.CreatedDate)
                .IsOptional();

            Property(model => model.LastUpdatedDate)
                .IsOptional();

            //public string SenderUserName { get; set; }
            //  public string SenderEmail { get; set; }                 
            //public string Body { get; set; }
        }
    }
}
