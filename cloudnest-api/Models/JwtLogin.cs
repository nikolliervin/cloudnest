using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudNest.Api.Models
{
    public class JwtLogin
    {
        public string token { get; set; }

        public DateTime? expiration { get; set; }
    }
}