using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PhotoFeatures
{
    public class GetAllPhotosCommand : IRequest<List<Photo>>
    {
        public class GetAllPhotosCommandHandler : IRequestHandler<GetAllPhotosCommand, List<Photo>>
        {
            private readonly IApplicationDbContext _context;
            public GetAllPhotosCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<List<Photo>> Handle(GetAllPhotosCommand command, CancellationToken cancellationToken)
            {
                return _context.Photos.Include(x=>x.User).Include(x=>x.Likes).ToList();
            }
        }
    }
}
