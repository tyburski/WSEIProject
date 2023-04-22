using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
        {
            private readonly IApplicationDbContext _context;
            public CreateUserCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(CreateUserCommand command, CancellationToken cancellationToken)
            {
                var check = _context.Users.FirstOrDefault(x => x.Username.Equals(command.Username));
                if(check is not null)
                {
                    return false;
                }
                var user = new User();
                user.Username = command.Username;
                user.Password = BCrypt.Net.BCrypt.HashPassword(command.Password);
                user.Role = "User";

                _context.Users.Add(user);
                await _context.SaveChanges();
                return true;
            }
        }
    }
}
