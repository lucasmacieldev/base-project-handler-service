using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence
{
    public class Context : DbContext, IContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseFacade DataBaseOrigim => throw new NotImplementedException();

        public Context(DbContextOptions<Context> options) : base(options) { }

        public Context() { }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.Entity.CreatedAt == DateTime.MinValue)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.State = EntityState.Added;
                }
                else if (entry.State == EntityState.Modified)
                    entry.Entity.ModifiedAt = DateTime.UtcNow;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        public IExecutionStrategy CreateExecutionStrategy() => Database.CreateExecutionStrategy();

        protected override void OnModelCreating(ModelBuilder modelBuilder) => _ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);

        public void SetModifiedState<T>(T entity) => base.Entry(entity).State = EntityState.Modified;

        public void AttachModelToContext<T>(T entity) => _ = base.Attach(entity);
    }
}
