

namespace Domain.Interfaces
{
    public interface ITimeRepository
    {
        System.Threading.Tasks.Task AddTimeAsync(Time time);
    }
}
