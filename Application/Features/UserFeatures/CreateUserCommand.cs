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
    public class CreateUserCommand : IRequest<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class AddPhotoCommandHandler : IRequestHandler<CreateUserCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public AddPhotoCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateUserCommand command, CancellationToken cancellationToken)
            {
                var user = new User();
                user.Username = command.Username;
                user.Password = command.Password;

                _context.Users.Add(user);
                await _context.SaveChanges();
                return user.Id;
            }
        }
    }
}
