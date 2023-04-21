namespace WebApi.Models
{
    public class AddPhotoModel
    {
        public IFormFile file { get; set; }
        public string Description { get; set; }
    }
}
