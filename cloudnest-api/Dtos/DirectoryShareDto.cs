using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudNest.Api.Models.Dtos
{
    public class DirectoryShareDto
    {
        public Guid DirectoryId { get; set; }
        public Guid UserId { get; set; }

        public DateTime? ExpiryDate { get; set; }

    }
}