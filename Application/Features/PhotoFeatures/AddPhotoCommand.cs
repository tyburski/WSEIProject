using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PhotoFeatures
{
    public class AddPhotoCommand : IRequest<int>
    {
        public IFormFile file { get; set; }
        public string Description { get; set; }
        public string token { get; set; }

        public class AddPhotoCommandHandler : IRequestHandler<AddPhotoCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public AddPhotoCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(AddPhotoCommand command, CancellationToken cancellationToken)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(command.token) as JwtSecurityToken;
                string userId = jwtToken.Claims.First(c => c.Type == "UserId").Value;
                string userName = jwtToken.Claims.First(c => c.Type == "Username").Value;

                var user = _context.Users.FirstOrDefault(x => x.Id.Equals(Int32.Parse(userId)));
                if (user is null) return 0;

                var photo = new Photo();

                photo.file = command.file;
                photo.Description = command.Description;
                photo.User = user;

                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\images\\{userName}");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                string uploadpath = Path.Combine(path, $"{Guid.NewGuid()}{Path.GetExtension(Path.GetFileName(command.file.FileName))}");

                var stream = new FileStream(uploadpath, FileMode.Create);
                await command.file.CopyToAsync(stream);

                photo.Path = uploadpath;

                _context.Photos.Add(photo);
                await _context.SaveChanges();
                return photo.Id;
            }
        }
    }
}
