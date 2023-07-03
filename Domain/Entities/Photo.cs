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
        public byte[] file { get; set; }
        public string Description { get; set; }


        public User User { get; set; }
       
        public virtual List<Comment>? Comments { get; set; }

        public virtual List<User>? Likes { get; set; }
    }
}
