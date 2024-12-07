using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudNest.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CloudNest.Api.Models
{
    [Keyless]
    public class DirectoryPermission : IAuditEntry
    {
        public Guid DirectoryId { get; set; }
        public Guid PermissionId { get; set; }  
        public DateTime? ExpirationDate { get; set; }  
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }

        public Directory Directory { get; set; }
        public UserPermissions Permission { get; set; }
    }

}