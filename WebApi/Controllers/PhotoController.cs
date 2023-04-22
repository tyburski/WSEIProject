using Application.Features.CommentFeatures;
using Application.Features.PhotoFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class PhotoController : BaseController
    {
        [Consumes("multipart/form-data")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm]AddPhotoModel model)
        {
            var authorization = Request.Headers[HeaderNames.Authorization];
            var command = new AddPhotoCommand();
            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var parameter = headerValue.Parameter;

                if (parameter is null) return Unauthorized();
                command.token = parameter;
            }
                        
            command.file = model.file;
            command.Description = model.Description;
            return Ok(await Mediator.Send(command));
        }
        [AllowAnonymous]
        [HttpGet("photos")]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetAllPhotosCommand();
            return Ok(await Mediator.Send(command));
        }
        [AllowAnonymous]
        [HttpGet("photo/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var command = new GetPhotoByIdCommand();
            command.PhotoId= id;
            return Ok(await Mediator.Send(command));
        }
        [AllowAnonymous]
        [HttpGet("photos/{username}")]
        public async Task<IActionResult> GetByUsername([FromRoute] string username)
        {
            var command = new GetUserPhotosCommand();
            command.Username= username;
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete("photo/delete/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var command = new DeletePhotoCommand();
            command.PhotoId = id;
            var authorization = Request.Headers[HeaderNames.Authorization];

            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var parameter = headerValue.Parameter;

                if (parameter is null) return Unauthorized();
                command.token = parameter;
            }
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("photo/{id}/createcomment")]
        public async Task<IActionResult> CreateComment([FromRoute] int id, [FromForm] string content)
        {
            var command = new CreateCommentCommand();
            
            var authorization = Request.Headers[HeaderNames.Authorization];

            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var parameter = headerValue.Parameter;

                if (parameter is null) return Unauthorized();
                command.token = parameter;
                command.PhotoId = id;
                command.Content = content;
            }
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("photo/{id}/like")]
        public async Task<IActionResult> LikePhoto([FromRoute] int id)
        {
            var command = new LikePhotoCommand();

            var authorization = Request.Headers[HeaderNames.Authorization];

            if (AuthenticationHeaderValue.TryParse(authorization, out var headerValue))
            {
                var parameter = headerValue.Parameter;

                if (parameter is null) return Unauthorized();
                command.token = parameter;
                command.PhotoId = id;
            }
            return Ok(await Mediator.Send(command));
        }
    }
}
