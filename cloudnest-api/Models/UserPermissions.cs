using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CloudNest.Api.Interfaces;

namespace CloudNest.Api.Models
{
    public class UserPermissions : IAuditEntry
    {
        [Key]
        public Guid PermissionId { get; set; }
        public Guid UserId { get; set; }
        public string PermissionType { get; set; }
        public string PermissionValue { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
    }

}