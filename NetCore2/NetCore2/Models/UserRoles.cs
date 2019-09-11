using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace NetCore2.Models
{

    public class UserRoles: IdentityUserRole<string>
    {

    }
}
