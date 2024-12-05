using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CloudNest.Api.Models;
using Microsoft.EntityFrameworkCore;
using CloudNest.Api.Models;

namespace CloudNest.Api.Helpers
{
    public class DirectoryPermissionHandler : AuthorizationHandler<DirectoryPermissionRequirement>
    {
        private readonly ApplicationDbContext _context;

        public DirectoryPermissionHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DirectoryPermissionRequirement requirement)
        {

            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return;
            }

            var directoryId = new Guid(context.Resource.ToString());

            var hasPermission = await _context.UserPermissions
                .AnyAsync(up => up.UserId == Guid.Parse(userId) &&
                               up.PermissionId == requirement.PermissionId &&
                               _context.DirectoryPermissions
                                    .Any(dp => dp.DirectoryId == directoryId && dp.PermissionId == up.PermissionId)); // Check if permission is linked to directory

            if (hasPermission)
            {
                context.Succeed(requirement);
            }
        }


    }
}
