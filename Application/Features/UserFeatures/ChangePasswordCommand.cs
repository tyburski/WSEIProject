using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures
{
    public class ChangePasswordCommand : IRequest<bool>
    {
        public string newPassword { get; set; }
        public string token { get; set; }

        public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, bool>
        {
            private readonly IApplicationDbContext _context;
            public ChangePasswordCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(command.token) as JwtSecurityToken;
                string userId = jwtToken.Claims.First(c => c.Type == "UserId").Value;

                var user = _context.Users.FirstOrDefault(x => x.Id.Equals(Int32.Parse(userId)));
                if (user is null)
                {
                    return false;
                }
                user.Password = BCrypt.Net.BCrypt.HashPassword(command.newPassword);

                await _context.SaveChanges();
                return true;
            }
        }
    }
}
