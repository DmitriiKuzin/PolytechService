using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore2.Models
{
    public class ApplicationSettings
    {
        public string JWTSecret { get; set; }
        public string ClientURL { get; set; }
    }
}
