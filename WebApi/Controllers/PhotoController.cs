using Application.Features.PhotoFeatures;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class PhotoController : BaseController
    {
        [HttpPost("add")]
        public async Task<IActionResult> Add(AddPhotoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
