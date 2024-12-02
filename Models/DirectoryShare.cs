using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudNest.Api.Interfaces;
using Directory = CloudNest.Api.Models;

namespace CloudNest.Api.Models
{
    public class DirectoryShare : IAuditEntry
    {
        public Guid DirectoryShareId { get; set; }
        public Guid DirectoryId { get; set; }
        public Guid UserId { get; set; }
        public DateTime? ExpiryDate { get; set; } = DateTime.MaxValue;
        public bool Deleted { get; set; }

        public virtual Directory Directory { get; set; }
        public virtual User User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
    }
}