using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PhotoFeatures
{
    public class LikePhotoCommand : IRequest<int>
    {
        public string token { get; set; }
        public int PhotoId { get; set; }

        public class LikePhotoCommandHandler : IRequestHandler<LikePhotoCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public LikePhotoCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(LikePhotoCommand command, CancellationToken cancellationToken)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(command.token) as JwtSecurityToken;
                string userId = jwtToken.Claims.First(c => c.Type == "UserId").Value;

                var user = _context.Users.FirstOrDefault(x => x.Id.Equals(Int32.Parse(userId)));
                if (user is null) return 0;

                var photo = _context.Photos.Include(x=>x.Likes).FirstOrDefault(x => x.Id.Equals(command.PhotoId));
                if(photo is null) return 0;

                if(photo.Likes.IndexOf(user)>-1)
                {
                    photo.Likes.Remove(user);
                }
                else photo.Likes.Add(user);

                await _context.SaveChanges();
                return photo.Id;
            }
        }
    }
}
