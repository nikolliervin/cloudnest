using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudNest.Api.Models.Dtos;

namespace CloudNest.Api.Interfaces
{
    public interface IPermissionsService
    {
        Task<ApiResponse<DirectoryPermissionsDto>> SetDirectoryPermissions(DirectoryPermissionsDto directoryPermissionsDto);
    }
}