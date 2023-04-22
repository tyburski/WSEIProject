using Application.Features.PhotoFeatures;
using Application.Features.UserFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : BaseController
    {
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> Add([FromForm]CreateUserModel model)
        {
            var command = new CreateUserCommand();
            command.Username= model.Username;
            command.Password= model.Password;
            return Ok(await Mediator.Send(command));
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm]LoginModel model)
        {
            var command = new LoginCommand();
            command.Username= model.Username;
            command.Password= model.Password;

            return Ok(await Mediator.Send(command));
        }
        [HttpPost("permissions/{id}")]
        public async Task<IActionResult> GrantPermissions([FromRoute]int id)
        {
            var command = new GrantPermissionsCommand();
            command.UserId= id;
            var authorization = Request.Headers[HeaderNames.Authorization];

            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var parameter = headerValue.Parameter;

                if (parameter is null) return Unauthorized();
                command.token = parameter;
            }
            return Ok(await Mediator.Send(command));
        }
        [HttpPut("newpassword")]
        public async Task<IActionResult> ChangePassword([FromForm]string newPassword)
        {
            var command = new ChangePasswordCommand();
            command.newPassword = newPassword;
            var authorization = Request.Headers[HeaderNames.Authorization];

            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var parameter = headerValue.Parameter;

                if (parameter is null) return Unauthorized();
                command.token = parameter;
            }
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var command = new DeleteUserCommand();

            var authorization = Request.Headers[HeaderNames.Authorization];

            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var parameter = headerValue.Parameter;

                if (parameter is null) return Unauthorized();
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(parameter) as JwtSecurityToken;
                string userRole = jwtToken.Claims.First(c => c.Type == "UserRole").Value;
                
                if(!userRole.Equals("Admin")) return Unauthorized();

                command.UserId = id;

                return Ok(await Mediator.Send(command));
            }
            return Unauthorized();
            
        }
    }
}
