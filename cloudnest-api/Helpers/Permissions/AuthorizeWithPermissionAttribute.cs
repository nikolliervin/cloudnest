using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CloudNest.Api.Helpers
{
    public class PermissionsAttribute : AuthorizeAttribute
    {
        public string Permission { get; }

        public PermissionsAttribute(string permission)
        {
            Permission = permission;
        }
    }

}