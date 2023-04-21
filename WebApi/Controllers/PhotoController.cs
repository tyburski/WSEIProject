using Application.Features.PhotoFeatures;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class PhotoController : BaseController
    {
        [Consumes("multipart/form-data")]
        [HttpPost("add")]
        public async Task<IActionResult> Add(IFormFile file)
        {
            var command = new AddPhotoCommand();
            command.file = file;
            return Ok(await Mediator.Send(command));
        }
    }
}
