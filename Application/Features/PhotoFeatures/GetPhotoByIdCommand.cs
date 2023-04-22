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
    public class GetPhotoByIdCommand : IRequest<Photo>
    {
        public int PhotoId {get; set;}

        public class GetPhotoByIdCommandHandler : IRequestHandler<GetPhotoByIdCommand, Photo>
        {
            private readonly IApplicationDbContext _context;
            public GetPhotoByIdCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<Photo> Handle(GetPhotoByIdCommand command, CancellationToken cancellationToken)
            {
                var photo = _context.Photos.Include(x => x.User).Include(x => x.Comments).ThenInclude(x=>x.Likes).Include(x => x.Likes).FirstOrDefault(x => x.Id.Equals(command.PhotoId));
                if (photo is not null) return photo;
                else return null;
            }
        }
    }
}
