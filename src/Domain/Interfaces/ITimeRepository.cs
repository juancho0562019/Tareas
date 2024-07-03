

namespace Domain.Interfaces
{
    public interface ITimeRepository
    {
        Task<Time> GetTimeByIdAsync(Guid timeId);
        System.Threading.Tasks.Task AddTimeAsync(Time time);
        System.Threading.Tasks.Task UpdateTimeAsync(Time time);
    }
}
