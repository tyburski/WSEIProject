using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PhotoFeatures
{
    public class AddPhotoCommand : IRequest<int>
    {
        public string Path { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; }

        public class AddPhotoCommandHandler : IRequestHandler<AddPhotoCommand, int>
        {
            private readonly IApplicationDbContext _context;
            public AddPhotoCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(AddPhotoCommand command, CancellationToken cancellationToken)
            {
                var photo = new Photo();
                photo.Path = command.Path;
                photo.Description = command.Description;
                photo.Likes = command.Likes;

                _context.Photos.Add(photo);
                await _context.SaveChanges();
                return photo.Id;
            }
        }
    }
}
