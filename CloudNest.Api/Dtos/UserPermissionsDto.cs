using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudNest.Api.Models.Dtos
{
    public class UserPermissionsDto
    {
        public Guid PermissionId { get; set; }
        public Guid UserId { get; set; }
        public string PermissionType { get; set; }
        public string PermissionValue { get; set; }
        public DateTime? ExpirationDate { get; set; }

    }
}