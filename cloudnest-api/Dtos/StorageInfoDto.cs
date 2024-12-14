using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudNest.Api.Dtos
{
    public class StorageInfoDto
    {
        public string DriveName { get; set; }
        public string DriveType { get; set; }
        public string FileSystem { get; set; }
        public double TotalSize { get; set; }
        public double FreeSpace { get; set; }
        public double UsedSpace { get; set; }
        public string MountPoint { get; set; }
    }
}