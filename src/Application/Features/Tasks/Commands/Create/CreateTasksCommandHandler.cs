using Domain.Interfaces;


namespace Application.Features.Tasks.Commands.Create
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDto>
    {
        private readonly ITaskRepository _taskRepository;

        public CreateTaskCommandHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = new Domain.Entities.Task
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Customer = request.Customer
            };

            await _taskRepository.AddTaskAsync(task);
            return new TaskDto 
            {
                Id = task.Id.ToString(),
                Name = task.Name,
                Description = task.Description,
                Customer = task.Customer
            };
        }
    }
}
