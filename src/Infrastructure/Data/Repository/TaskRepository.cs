using Application.Commons.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IApplicationDbContext _context;

        public TaskRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain.Entities.Task>> GetTasksAsync(bool expanded)
        {
            var query = _context.Tasks.AsQueryable();

            if (expanded)
            {
                query = query.Include(t => t.Times);
            }

            return await query.ToListAsync();
        }

        public async System.Threading.Tasks.Task AddTaskAsync(Domain.Entities.Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync(new CancellationToken());
            
        }

        public async Task<Domain.Entities.Task?> GetTaskWithTimesByIdAsync(Guid taskId)
        {
            return await _context.Tasks
                .Include(t => t.Times)
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<Domain.Entities.Task?> GetTaskByIdAsync(Guid taskId)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }
    }
}
