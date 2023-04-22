using Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
        {
            private readonly IApplicationDbContext _context;
            public DeleteUserCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
            {
                var user = _context.Users.FirstOrDefault(x => x.Id.Equals(command.UserId));
                if (user is null)
                {
                    return false;
                }
                _context.Users.Remove(user);

                await _context.SaveChanges();
                return true;
            }
        }
    }
}
