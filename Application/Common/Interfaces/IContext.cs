using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IContext
    {
        DbSet<Domain.Entities.Client> Clients { get; set; }
        DbSet<Domain.Entities.User> Users { get; set; }
        IExecutionStrategy CreateExecutionStrategy();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void SetModifiedState<T>(T entity);
        void AttachModelToContext<T>(T entity);
        DatabaseFacade DataBaseOrigim { get; }
    }
}
