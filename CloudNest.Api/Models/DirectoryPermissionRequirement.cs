using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CloudNest.Api.Models
{
    public class DirectoryPermissionRequirement : IAuthorizationRequirement
    {
        public Guid DirectoryId { get; }
        public Guid PermissionId { get; }
        public string PermissionType { get; }

        public DirectoryPermissionRequirement(Guid directoryId, Guid permissionId, string permissionType)
        {
            DirectoryId = directoryId;
            PermissionId = permissionId;
            PermissionType = permissionType;
        }
    }


}