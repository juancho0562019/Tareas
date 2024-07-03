using Application.Commons.Exceptions;
using Application.Features.Tasks;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Times.Commands.Update
{
    public class UpdateTimeCommand : IRequest<ResponseTimeDto>
    {
        public string TaskId { get; set; }
        public string TimeId { get; set; }
        public string Description { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class UpdateTimeCommandHandler : IRequestHandler<UpdateTimeCommand, ResponseTimeDto>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITimeRepository _timeRepository;

        public UpdateTimeCommandHandler(ITaskRepository taskRepository, ITimeRepository timeRepository)
        {
            _taskRepository = taskRepository;
            _timeRepository = timeRepository;
        }

        public async Task<ResponseTimeDto> Handle(UpdateTimeCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetTaskByIdAsync(Guid.Parse(request.TaskId));

            if (task == null)
            {
                throw new NotFoundException("Task not found");
            }

            var time = await _timeRepository.GetTimeByIdAsync(Guid.Parse(request.TimeId));
            if (time == null || time.TaskId != task.Id)
            {
                throw new NotFoundException("Time not found or does not belong to the task");
            }

            time.Description = request.Description;
            time.BeginDate = request.BeginDate;
            time.EndDate = request.EndDate;

            await _timeRepository.UpdateTimeAsync(time);

            return new ResponseTimeDto
            {
                
                Description = time.Description,
                BeginDate = time.BeginDate,
                EndDate = time.EndDate,
            };
        }
    }
}
