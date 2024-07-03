using Domain.Entities;

namespace Application.Commons.Interfaces
{
    public interface IApplicationDbContext
    {

        DbSet<Time> Times { get; }
        DbSet<Domain.Entities.Task> Tasks { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
