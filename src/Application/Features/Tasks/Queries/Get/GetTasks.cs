using Domain.Interfaces;

namespace Application.Features.Tasks.Queries.Get
{
    public class GetTasks : IRequest<IEnumerable<TaskDto>>
    {
        public string? Expanded { get; set; }
    }

    public class GetTasksQueryHandler : IRequestHandler<GetTasks, IEnumerable<TaskDto>>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTasksQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<IEnumerable<TaskDto>> Handle(GetTasks request, CancellationToken cancellationToken)
        {
            bool includeTimes = request.Expanded?.Equals("times", StringComparison.OrdinalIgnoreCase) ?? false;
            var tasks = await _taskRepository.GetTasksAsync(includeTimes);

            if (includeTimes)
            {
                return tasks.Select(task => new TaskWithTimesDto
                {
                    Id = task.Id.ToString(),
                    Name = task.Name,
                    Description = task.Description,
                    Customer = task.Customer,
                    Times = task.Times?.Select(t => new TimeDto
                    {
                        Id = t.Id.ToString(),
                        Description = t.Description,
                        BeginDate = t.BeginDate,
                        EndDate = t.EndDate,
                        SpentTime = ((t.EndDate ?? DateTime.UtcNow) - t.BeginDate).TotalHours
                    }).ToList() ?? default
                });
            }
            else
            {
                return tasks.Select(task => new TaskDto
                {
                    Id = task.Id.ToString(),
                    Name = task.Name,
                    Description = task.Description,
                    Customer = task.Customer
                });
            }
        }
    }
}
