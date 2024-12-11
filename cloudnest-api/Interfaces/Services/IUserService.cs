using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudNest.Api.Models;

namespace CloudNest.Api.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<UpdateUserDto>> UpdateUserAsync(UpdateUserDto request);
    }
}