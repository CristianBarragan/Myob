using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataModelEntities;
using Microsoft.EntityFrameworkCore;

namespace DomainSqlServer
{
    public class DomainModelContext : DbContext
    {
        public DomainModelContext(DbContextOptions<DomainModelContext> options) : base(options)
        { }

        public DbSet<File> Files { get; set; }

        public DbSet<FilePayRoll> FilePayrolls { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<File>().HasKey(m => m.Id);
            builder.Entity<FilePayRoll>().HasKey(m => m.Id);

            //Versioning properties
            builder.Entity<File>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<FilePayRoll>().Property<DateTime>("UpdatedTimestamp");

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            updateProperty<File>();
            updateProperty<FilePayRoll>();

            return base.SaveChanges();
        }

        private void updateProperty<T>() where T : class
        {
            var modified =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modified)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}
