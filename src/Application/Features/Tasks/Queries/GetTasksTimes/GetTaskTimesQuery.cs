using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Application.Features.Tasks.Queries.GetTasksTimes
{
    public class GetTaskTimesQuery : IRequest<TaskWithTimesDto>
    {
        [Required]
        public Guid TaskId { get; set; }
    }

    public class GetTaskTimesQueryHandler : IRequestHandler<GetTaskTimesQuery, TaskWithTimesDto>
    {
        private readonly ITaskRepository _taskRepository;

        public GetTaskTimesQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskWithTimesDto> Handle(GetTaskTimesQuery request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetTaskWithTimesByIdAsync(request.TaskId);

            if (task == null)
            {
                return null;
            }

            return new TaskWithTimesDto
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
                }).ToList()
            };
        }
    }

}
