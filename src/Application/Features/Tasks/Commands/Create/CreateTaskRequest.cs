
namespace Application.Features.Tasks.Commands.Create
{
    public class CreateTaskCommand : IRequest<TaskDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Customer { get; set; }
    }
}
