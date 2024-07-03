using Web.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [ApiExceptionFilter]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender? _mediator;
        private IConfiguration? _configuration;

        protected IConfiguration Configuration => _configuration ??= HttpContext.RequestServices.GetService<IConfiguration>();
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        protected virtual async Task<ActionResult> Send<Request, Response>(Request command) where Request : IRequest<Response>
        {
            return !ModelState.IsValid ? BadRequest(ModelState) : Ok(await Mediator.Send(command));
        }
        protected virtual async Task<ActionResult> Command<Request, Response>(Request command) where Request : IRequest<Response>
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           return Ok(await Mediator.Send(command));
          




        }
    }

}
