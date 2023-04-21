using Domain.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Photo : Base
    {
        [NotMapped]
        public IFormFile file { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; } = 0;

        public User? User { get; set; }
        public List<Comment>? Comments { get; set; } 
    }
}
