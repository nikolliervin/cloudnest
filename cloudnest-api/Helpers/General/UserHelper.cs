using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CloudNest.Api.Dtos;


namespace CloudNest.Api.Helpers
{
    public class UserHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid GetCurrentUserId()
        {
            if (_httpContextAccessor == null)
            {
                throw new InvalidOperationException("HttpContextAccessor is not set. Make sure to call SetHttpContextAccessor in Startup.");
            }

            var userClaims = _httpContextAccessor.HttpContext?.User?.Claims;
            var userIdClaim = userClaims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim != null)
            {
                return Guid.Parse(userIdClaim.Value);
            }

            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        public List<StorageInfoDto> GetStorageInfo()
        {
            var drives = DriveInfo.GetDrives();
            var driveInfoList = new List<StorageInfoDto>();

            foreach (var drive in drives)
            {
                if (drive.IsReady)
                {
                    driveInfoList.Add(new StorageInfoDto
                    {
                        DriveName = drive.Name,
                        DriveType = drive.DriveType.ToString(),
                        FileSystem = drive.DriveFormat,
                        TotalSize = Math.Round(drive.TotalSize / (1024.0 * 1024 * 1024), 2),
                        FreeSpace = Math.Round(drive.TotalFreeSpace / (1024.0 * 1024 * 1024), 2),
                        UsedSpace = Math.Round((drive.TotalSize - drive.TotalFreeSpace) / (1024.0 * 1024 * 1024), 2),
                        MountPoint = drive.Name
                    });
                }
            }

            return driveInfoList
    .Where(drive => drive.DriveType == "Fixed" 
                    && !string.IsNullOrEmpty(drive.FileSystem) 
                    && drive.TotalSize > 0
                    && drive.FreeSpace > 2
                    && !drive.MountPoint.StartsWith("/var/lib/docker") 
                    && !drive.MountPoint.Contains("overlay")) 
    .ToList();

        }
    }
}