using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CommentFeatures
{
    public class LikeCommentCommand : IRequest<int>
    {
        public string token { get; set; }
        public int CommentId { get; set; }

        public class LikeCommentCommandHandler : IRequestHandler<LikeCommentCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public LikeCommentCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(LikeCommentCommand command, CancellationToken cancellationToken)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(command.token) as JwtSecurityToken;
                string userId = jwtToken.Claims.First(c => c.Type == "UserId").Value;

                var user = _context.Users.FirstOrDefault(x => x.Id.Equals(Int32.Parse(userId)));
                if (user is null) return 0;

                var comment = _context.Comments.Include(x => x.Likes).FirstOrDefault(x => x.Id.Equals(command.CommentId));
                if (comment is null) return 0;

                if (comment.isDeleted) return 0;

                if (comment.Likes.IndexOf(user) > -1)
                {
                    comment.Likes.Remove(user);
                }
                else comment.Likes.Add(user);

                await _context.SaveChanges();
                return comment.Id;
            }
        }
    }
}
