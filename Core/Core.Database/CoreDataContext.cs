using Core.Database.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.App.CurrentUser;

namespace Core.Database
{
    public class CoreDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        private readonly ICurrentUserLocator currentUserLocator;

        protected CoreDataContext()
        {

        }

        public CoreDataContext(DbContextOptions options, ICurrentUserLocator currentUserLocator)
            : base(options)
        {
            this.currentUserLocator = currentUserLocator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConventions();
            User.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            foreach (var change in ChangeTracker.Entries().ToList())
            {
                if (!(change.State == EntityState.Added || change.State == EntityState.Modified))
                    continue;

                if (change.Entity is AuditEntity auditEntity && change.State == EntityState.Modified)
                    auditEntity.UpdatedOn = System.DateTime.UtcNow;


                if (change.Entity is FullAuditEntity fullAuditEntity)
                {
                    if (change.State == EntityState.Added)
                        fullAuditEntity.CreatedById = currentUserLocator.UserId;

                    fullAuditEntity.UpdatedById = currentUserLocator.UserId;
                }
            }
        }
    }
}
