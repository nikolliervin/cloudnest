using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudNest.Api.Interfaces
{
    public interface IAuditEntry
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        Guid CreatedBy { get; set; }
        Guid? UpdatedBy { get; set; }
        bool Deleted { get; set; }
        DateTime? DeletedAt { get; set; }
        Guid? DeletedBy { get; set; }

    }
}