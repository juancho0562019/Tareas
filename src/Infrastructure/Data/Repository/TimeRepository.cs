using Application.Commons.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data.Repository
{
    public class TimeRepository : ITimeRepository
    {
        private readonly IApplicationDbContext _context;

        public TimeRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public async System.Threading.Tasks.Task<Time> GetTimeByIdAsync(Guid timeId)
        {
            return await _context.Times.FindAsync(timeId);
        }
        public async System.Threading.Tasks.Task AddTimeAsync(Time time)
        {
            await _context.Times.AddAsync(time);
            await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async System.Threading.Tasks.Task UpdateTimeAsync(Time time)
        {
            _context.Times.Update(time);
            await _context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
