using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore2.Models
{
    //For transfer to client
    public class AppUserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public IdentRole Role { get; set; }
        public int? Room { get; set; }
        public int? DormId { get; set; }
    }
}
