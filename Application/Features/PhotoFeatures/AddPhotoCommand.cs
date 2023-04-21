﻿using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PhotoFeatures
{
    public class AddPhotoCommand : IRequest<int>
    {
        public IFormFile file { get; set; }
        public string Description { get; set; }

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

                photo.file = command.file;
                photo.Description = command.Description;
                
                string fileName = command.file.FileName;

                fileName = Path.GetFileName(fileName);

                string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                var stream = new FileStream(uploadpath, FileMode.Create);

                command.file.CopyToAsync(stream);

                photo.Path = uploadpath;

                _context.Photos.Add(photo);
                await _context.SaveChanges();
                return photo.Id;
            }
        }
    }
}
