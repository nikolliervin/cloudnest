using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CloudNest.Api.Helpers
{
    public class DirectoryPermissionAttribute : AuthorizeAttribute, IAuthorizationRequirement
    {
        public string Permission { get; }

        public DirectoryPermissionAttribute(string permission)
        {
            Permission = permission;
        }
    }

}