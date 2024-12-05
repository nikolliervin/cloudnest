using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CloudNest.Api.Models
{
    public class UserClaim : IdentityUserClaim<Guid>
    {
        public Guid? DirectoryId { get; set; }
    }
}