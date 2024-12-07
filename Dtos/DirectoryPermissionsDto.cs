using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudNest.Api.Models.Dtos
{
    public class DirectoryPermissionsDto
    {
        public Guid DirectoryId { get; set; }
        public UserPermissionsDto Permission { get; set; }

    }
}