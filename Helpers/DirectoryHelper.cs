using System;
using System.IO;
using CloudNest.Api.Models;
using CloudNest.Api.Models.Dtos;
using Dir = System.IO.Directory;
using Directory = CloudNest.Api.Models.Directory;

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


        public static bool UpdateDirectoryInFileSystem(DirectoryDto directoryDto, Directory existingDirectory, string baseDirectoryPath)
        {
            var userDirectoryPath = Path.Combine(baseDirectoryPath, directoryDto.UserId.ToString());

            if (!Dir.Exists(userDirectoryPath))
                return false;

            var oldDirectoryPath = Path.Combine(userDirectoryPath, existingDirectory.FullPath);

            if (!Dir.Exists(oldDirectoryPath))
                return false;
            string updatedFullPath = directoryDto.Name;

            if (directoryDto.ParentDirectoryId != existingDirectory.ParentDirectoryId)
                updatedFullPath = Path.Combine(directoryDto.ParentDirectoryId.ToString(), directoryDto.Name);
            else
                updatedFullPath = Path.Combine(Path.GetDirectoryName(existingDirectory.FullPath), directoryDto.Name);


            var newDirectoryPath = Path.Combine(userDirectoryPath, updatedFullPath);

            if (Dir.Exists(newDirectoryPath))
                return false;

            var parentDirectoryPath = Path.GetDirectoryName(newDirectoryPath);
            if (!string.IsNullOrEmpty(parentDirectoryPath) && !Dir.Exists(parentDirectoryPath))
                Dir.CreateDirectory(parentDirectoryPath);

            Dir.Move(oldDirectoryPath, newDirectoryPath);

            existingDirectory.Name = directoryDto.Name;
            existingDirectory.ParentDirectoryId = directoryDto.ParentDirectoryId;
            existingDirectory.FullPath = updatedFullPath;
            existingDirectory.UpdatedAt = DateTime.UtcNow;
            existingDirectory.UpdatedBy = directoryDto.UserId;

            return true;
        }


    }


}
