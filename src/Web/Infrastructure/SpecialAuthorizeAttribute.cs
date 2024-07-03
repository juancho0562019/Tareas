using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Web.Infrastructure
{
    public class SpecialAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {

        public string Feature { get; set; } = string.Empty;


        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {


            var jwt = context.HttpContext.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(jwt))
            {
                SetUnauthorizedResult(context, "Missing Jwt Token");
                return;
            }
            //TODO: check Feature permission

            var decodedToken = new JwtSecurityToken(jwt);

            var userIdClaim = decodedToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            var userId = userIdClaim?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                SetUnauthorizedResult(context, "Missing UserId");
                return;
            }

            context.HttpContext.Items["UserId"] = userId;
        }

        private void SetUnauthorizedResult(AuthorizationFilterContext context, string errorTitle)
        {
            
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = errorTitle,
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
            };
            
            context.Result = new UnauthorizedObjectResult(details);
        }
    }
}
