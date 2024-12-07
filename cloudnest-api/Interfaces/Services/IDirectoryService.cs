using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudNest.Api.Models.Dtos;

namespace CloudNest.Api.Interfaces
{
    public interface IDirectoryService
    {
        Task<ApiResponse<DirectoryDto>> CreateDirectoryAsync(DirectoryDto directoryDto);
        Task<ApiResponse<DirectoryDto>> UpdateDirectoryAsync(DirectoryDto directoryDto);
        Task<ApiResponse<bool>> DeleteDirectoryAsync(Guid id);
        Task<ApiResponse<DirectoryDto>> GetDirectoryAsync(Guid id);
        Task<ApiResponse<IEnumerable<DirectoryDto>>> GetUserDirectoriesAsync();
    }
}