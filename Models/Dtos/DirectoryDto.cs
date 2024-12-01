using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudNest.Api.Models.Dtos
{
    public class DirectoryDto
    {
        public Guid DirectoryId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public Guid? ParentDirectoryId { get; set; }
        public string FullPath { get; set; }
        public bool Deleted { get; set; }

    }
}