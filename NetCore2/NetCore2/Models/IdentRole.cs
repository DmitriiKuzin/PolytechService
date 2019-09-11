using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore2.Models
{
    public class IdentRole : IdentityRole
    {
        public string rusName { get; set; }

    }
}
