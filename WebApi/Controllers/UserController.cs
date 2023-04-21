using Application.Features.UserFeatures;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class UserController : BaseController
    {
        [HttpPost("create")]
        public async Task<IActionResult> Add(CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
