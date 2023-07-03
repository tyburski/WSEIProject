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

                using (var memoryStream = new MemoryStream())
                {
                    await command.file.CopyToAsync(memoryStream);

                    if (memoryStream.Length < 2097152)
                    {
                        var user = _context.Users.FirstOrDefault(x => x.Id.Equals(Int32.Parse(userId)));
                        if (user is null) return default;

                        var photo = new Photo()
                        {
                            file = memoryStream.ToArray(),
                            Description = command.Description,
                            User = user
                        };

                        _context.Photos.Add(photo);

                        await _context.SaveChanges();

                        return photo.Id;
                    }
                    else
                    {
                        return default;
                    }
                }
                
            }
        }
    }
}
