using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PhotoFeatures
{
    public class GetUserPhotosCommand : IRequest<List<Photo>>
    {
        public string Username { get; set; }

        public class GetUserPhotosCommandHandler : IRequestHandler<GetUserPhotosCommand, List<Photo>>
        {
            private readonly IApplicationDbContext _context;
            public GetUserPhotosCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<Photo>> Handle(GetUserPhotosCommand command, CancellationToken cancellationToken)
            {
                return _context.Photos.Include(x => x.User).Include(x => x.Likes).Where(x=>x.User.Username.Equals(command.Username)).ToList();
            }
        }
    }
}
