using Application.Features.PhotoFeatures;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class PhotoController : BaseController
    {
        [Consumes("multipart/form-data")]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm]AddPhotoModel model)
        {
            var command = new AddPhotoCommand();
            command.file = model.file;
            command.Description = model.Description;
            return Ok(await Mediator.Send(command));
        }
    }
}
