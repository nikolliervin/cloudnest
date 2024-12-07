using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudNest.Api.Interfaces;

namespace CloudNest.Api.Models
{
    public class Directory : IAuditEntry
    {
        public Guid DirectoryId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public Guid? ParentDirectoryId { get; set; }
        public string FullPath { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }

        public User User { get; set; }
        public Directory ParentDirectory { get; set; }
        public ICollection<Directory> Subdirectories { get; set; } = new List<Directory>();
        public ICollection<DirectoryShare> DirectoryShares { get; set; }
    }

}