using Microsoft.AspNet.Identity.EntityFramework;
using SakaryaBel.Web.DomainModel.Entities;
using SakaryaBel.Web.Mapping;
using SakaryaBel.Web.Migrations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SakaryaBel.Web.Identity
{
    public class BlogContext : IdentityDbContext<ApplicationUser>
    {
        public BlogContext()
            : base("SakaryaBelConnection")
        {
            //Database.SetInitializer<BlogContext>(null);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogContext, Configuration>("SakaryaBelConnection"));
        }

        public DbSet<Message> Message { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<File> File { get; set; }

        //public DbSet<Entry> Entries { get; set; }
        //public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ApplicationUser>()
                .HasOptional<File>(u => u.File)
                .WithOptionalPrincipal();

            modelBuilder.Configurations.Add(new MessageMap());
            modelBuilder.Configurations.Add(new ActivityMap());
            modelBuilder.Configurations.Add(new FileMap());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}