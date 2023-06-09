﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : Base
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public virtual List<Photo>? Photos { get; set; } 

        public virtual List<Comment>? Comments { get; set; } 
 
    }
}
