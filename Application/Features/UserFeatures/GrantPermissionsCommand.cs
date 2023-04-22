using Application.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures
{
    public class GrantPermissionsCommand : IRequest<bool>
    {
        public int UserId { get; set; }
        public string token { get; set; }

        public class GrantPermissionsCommandHandler : IRequestHandler<GrantPermissionsCommand, bool>
        {
            private readonly IApplicationDbContext _context;
            public GrantPermissionsCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<bool> Handle(GrantPermissionsCommand command, CancellationToken cancellationToken)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadToken(command.token) as JwtSecurityToken;
                string userRole = jwtToken.Claims.First(c => c.Type == "UserRole").Value;

                var user = _context.Users.FirstOrDefault(x => x.Id.Equals(command.UserId));
                if (user is null) return false;
                if (!userRole.Equals("Admin")) return false;

                if (user.Role.Equals("User")) user.Role = "Admin";
                else if (user.Role.Equals("Admin")) user.Role = "User";
                await _context.SaveChanges();
                return true;

                
            }
        }
    }
}
