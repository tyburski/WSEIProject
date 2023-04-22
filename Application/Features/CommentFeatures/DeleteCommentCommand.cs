using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CommentFeatures
{
    public class DeleteCommentCommand : IRequest<int>
    {
        public int CommentId { get; set; }
        public string token { get; set; }

        public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public DeleteCommentCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(command.token) as JwtSecurityToken;
                string userId = jwtToken.Claims.First(c => c.Type == "UserId").Value;
                string userRole = jwtToken.Claims.First(c => c.Type == "UserRole").Value;

                var user = _context.Users.FirstOrDefault(x => x.Id.Equals(Int32.Parse(userId)));
                if (user is null) return 0;

                var comment = _context.Comments.FirstOrDefault(x => x.Id.Equals(command.CommentId));
                if (comment is null) return 0;

                if (!user.Id.Equals(comment.Id))
                {
                    if (!userRole.Equals("Admin")) return 0;
                    else
                    {
                        comment.Content = "Komentarz usunięty";
                        comment.isDeleted = true;
                        comment.Likes.Clear();

                        await _context.SaveChanges();
                    }
                }
                else
                {
                    comment.Content = "Komentarz usunięty";
                    comment.isDeleted = true;
                    comment.Likes.Clear();

                    await _context.SaveChanges();
                }
                
                return comment.Id;
            }
        }
    }
}
