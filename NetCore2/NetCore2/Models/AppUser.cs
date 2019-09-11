using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore2.Models
{
    //Make modifications in identity database Users table
    public class AppUser : IdentityUser
    {
        [Column(TypeName ="nvarchar(150)")]
        public string FullName { get; set; }
        public int? DormId { get; set; }
        public int? Room { get; set; }

    }

   
    
}
