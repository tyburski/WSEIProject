using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PhotoFeatures
{
    public class DeletePhotoCommand : IRequest<bool>
    {
        public string token { get; set; }
        public int PhotoId { get; set; }

        public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, bool>
        {
            private readonly IApplicationDbContext _context;
            public DeletePhotoCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(DeletePhotoCommand command, CancellationToken cancellationToken)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(command.token) as JwtSecurityToken;
                string userId = jwtToken.Claims.First(c => c.Type == "UserId").Value;
                string userRole = jwtToken.Claims.First(c => c.Type == "UserRole").Value;


                var photo = _context.Photos.Include(x=>x.User).FirstOrDefault(x=>x.Id.Equals(command.PhotoId));
                if (photo is null) return false;
                if (!photo.User.Id.Equals(Int32.Parse(userId)))
                {
                    if (!userRole.Equals("Admin")) return false;
                    else
                    {
                        _context.Photos.Remove(photo);
                        await _context.SaveChanges();
                        return true;
                    }
                }
                else
                {
                    _context.Photos.Remove(photo);
                    await _context.SaveChanges();
                    return true;
                }
            }
        }
    }
}
