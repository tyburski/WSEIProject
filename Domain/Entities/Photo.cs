using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Photo : Base
    {
        public string Path { get; set; }
        public string Description { get; set; }
        public int Likes { get; set; } = 0;
    }
}
