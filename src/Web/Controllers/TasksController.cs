using Application.Features.Tasks;
using Application.Features.Tasks.Commands.Create;
using Application.Features.Tasks.Queries.Get;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    /// <summary>
    /// Controlador para la gestión de pólizas
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TasksController : ApiControllerBase
    {
        

    
        [HttpGet()]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Get([FromQuery] GetTasks query)
        {
            return await base.Send<GetTasks, IEnumerable<TaskDto>>(query);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            return await base.Command<CreateTaskCommand, TaskDto>(command);
            
        }
    }
}
