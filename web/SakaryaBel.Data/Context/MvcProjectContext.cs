using SakaryaBel.Core.DomainModel.Entities;
using SakaryaBel.Data.Mapping;
using System.Data.Entity;

namespace SakaryaBel.Data.Context
{
    public class MvcProjectContext : DbContext
    {
        public MvcProjectContext()
            : base("SakaryaBelConnection")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Activity> Activity { get; set; }        
        public DbSet<File> File { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {          
            modelBuilder.Configurations.Add(new ActivityMap());            
            modelBuilder.Configurations.Add(new FileMap());
                                                
            base.OnModelCreating(modelBuilder);
        }
    }
}
