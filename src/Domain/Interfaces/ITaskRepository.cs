﻿
namespace Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Domain.Entities.Task>> GetTasksAsync(bool expanded);
        System.Threading.Tasks.Task AddTaskAsync(Domain.Entities.Task task);
        Task<Domain.Entities.Task?> GetTaskWithTimesByIdAsync(Guid taskId);
        Task<Domain.Entities.Task> GetTaskByIdAsync(Guid taskId);
    }
}
