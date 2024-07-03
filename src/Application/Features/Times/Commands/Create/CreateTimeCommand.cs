using Application.Commons.Exceptions;
using Application.Features.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Times.Commands.Create
{
    public class CreateTimeCommand : IRequest<CreateTimeDto>
    {
        public string TaskId { get; set; }
        public string Description { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class CreateTimeDto
    {
        public string Description { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
     
    }
    public class CreateTimeCommandHandler : IRequestHandler<CreateTimeCommand, CreateTimeDto>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITimeRepository _timeRepository;

        public CreateTimeCommandHandler(ITaskRepository taskRepository, ITimeRepository timeRepository)
        {
            _taskRepository = taskRepository;
            _timeRepository = timeRepository;
        }

        public async Task<CreateTimeDto> Handle(CreateTimeCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetTaskByIdAsync(Guid.Parse(request.TaskId));

            if (task == null)
            {
                throw new NotFoundException("Task not found");
            }

            var time = new Time
            {
                Id = Guid.NewGuid(),
                TaskId = task.Id,
                Description = request.Description,
                BeginDate = request.BeginDate,
                EndDate = request.EndDate
            };

            await _timeRepository.AddTimeAsync(time);

            return new CreateTimeDto
            {
               
                Description = time.Description,
                BeginDate = time.BeginDate,
                EndDate = time.EndDate,
               
            };
        }
    }
}
