using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CommentFeatures
{
    public class CreateCommentCommand : IRequest<int>
    {
        public int PhotoId { get; set; }
        public string token { get; set; }
        public string Content { get; set; }

        public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public CreateCommentCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateCommentCommand command, CancellationToken cancellationToken)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(command.token) as JwtSecurityToken;
                string userId = jwtToken.Claims.First(c => c.Type == "UserId").Value;

                var user = _context.Users.FirstOrDefault(x => x.Id.Equals(Int32.Parse(userId)));
                if (user is null) return 0;

                var photo = _context.Photos.FirstOrDefault(x => x.Id.Equals(command.PhotoId));
                if (photo is null) return 0;

                var comment = new Comment();
                comment.Content = command.Content;
                comment.User = user;
                comment.Photo = photo;
                comment.isDeleted = false;

                _context.Comments.Add(comment);
                await _context.SaveChanges();
                return photo.Id;
            }
        }
    }
}
