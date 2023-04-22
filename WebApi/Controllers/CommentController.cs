using Application.Features.CommentFeatures;
using Application.Features.PhotoFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;

namespace WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class CommentController : BaseController
    {
        [HttpPost("comment/{id}/like")]
        public async Task<IActionResult> LikeComment([FromRoute] int id)
        {
            var command = new LikeCommentCommand();

            var authorization = Request.Headers[HeaderNames.Authorization];

            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var parameter = headerValue.Parameter;

                if (parameter is null) return Unauthorized();
                command.token = parameter;
                command.CommentId = id;
            }
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("comment/{id}/delete")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            var command = new DeleteCommentCommand();

            var authorization = Request.Headers[HeaderNames.Authorization];

            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var parameter = headerValue.Parameter;

                if (parameter is null) return Unauthorized();
                command.token = parameter;
                command.CommentId = id;
            }
            return Ok(await Mediator.Send(command));
        }

    }
}
