using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment : Base
    {
        public string Content { get; set; }

        public Photo Photo { get; set; }

        public  User User { get; set; }

        public bool isDeleted { get; set; }

        public virtual List<User>? Likes { get; set; }
    }
}
