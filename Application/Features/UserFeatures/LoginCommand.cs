using Application.Interfaces;
using Domain.Entities;
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
    public class LoginCommand : IRequest<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
        {
            private readonly IApplicationDbContext _context;
            public LoginCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken)
            {
                var user = _context.Users.FirstOrDefault(x => x.Username.Equals(command.Username));
                if (user is null) return String.Empty;

                if (!BCrypt.Net.BCrypt.Verify(command.Password, user.Password)) return String.Empty;

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMySecretKey"));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[] {
                    new Claim("Username", user.Username),
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("UserRole", user.Role),
                };

                var token = new JwtSecurityToken("test.com",
                    "test.com",
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }
}
