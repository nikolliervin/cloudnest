using System;
using System.IO;
using CloudNest.Api.Models;
using CloudNest.Api.Models.Dtos;

using Dir = System.IO.Directory;

namespace CloudNest.Api.Helpers
{
    public static class DirectoryHelper
    {
        public static bool CreateDirectoryInFileSystem(DirectoryDto directoryDto, string baseDirectoryPath)
        {
            var userDirectoryPath = Path.Combine(baseDirectoryPath, directoryDto.UserId.ToString());

            if (!Dir.Exists(userDirectoryPath))
            {
                Dir.CreateDirectory(userDirectoryPath);
            }

            var directoryPath = Path.Combine(userDirectoryPath, directoryDto.FullPath);

            if (Dir.Exists(directoryPath))
            {
                return false;
            }

            var parentDirectoryPath = Path.GetDirectoryName(directoryPath);
            if (!string.IsNullOrEmpty(parentDirectoryPath) && !Dir.Exists(parentDirectoryPath))
            {
                Dir.CreateDirectory(parentDirectoryPath);
            }

            Dir.CreateDirectory(directoryPath);
            return true;
        }
    }


}
