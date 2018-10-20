using greenlit.Helpers.Timestamps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace greenlit.Entities
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            var added = ChangeTracker.Entries<IAuditableModel>().Where(e => e.State == EntityState.Added).ToList();

            added.ForEach(e =>
            {
                e.Property(x => x.CreatedAt).CurrentValue = DateTime.UtcNow;
                e.Property(x => x.CreatedAt).IsModified = true;
            });

            var modified = ChangeTracker.Entries<IAuditableModel>().Where(e => e.State == EntityState.Modified).ToList();

            modified.ForEach(e =>
            {
                e.Property(x => x.UpdatedAt).CurrentValue = DateTime.UtcNow;
                e.Property(x => x.UpdatedAt).IsModified = true;

                e.Property(x => x.CreatedAt).CurrentValue = e.Property(x => x.CreatedAt).OriginalValue;
                e.Property(x => x.CreatedAt).IsModified = false;
            });

            return base.SaveChanges();
        }
    }
}
