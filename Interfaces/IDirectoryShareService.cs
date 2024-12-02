using CloudNest.Api.Models.Dtos;
using System;
using System.Threading.Tasks;

namespace CloudNest.Api.Interfaces
{
    public interface IDirectoryShareService
    {
        Task<ApiResponse<bool>> ShareDirectoryAsync(DirectoryShareDto directoryShareDto);
        Task<ApiResponse<bool>> RemoveShareDirectoryAsync(DirectoryShareDto directoryShareDto);
    }
}
