using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading.Tasks;
using AutoMapper;
using CloudNest.Api.Interfaces;
using CloudNest.Api.Models;
using CloudNest.Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace CloudNest.Api.Services
{
    public class PermissionsService : IPermissionsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public PermissionsService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ApiResponse<DirectoryPermissionsDto>> SetDirectoryPermissions(DirectoryPermissionsDto directoryPermissionsDto)
        {
            var exists = await _context.UserPermissions
                        .Join(_context.DirectoryPermissions,
                            up => up.PermissionId,
                            dp => dp.PermissionId,
                            (up, dp) => new { UserPermission = up, DirectoryPermission = dp })
                        .Where(x => x.UserPermission.PermissionValue == directoryPermissionsDto.Permission.PermissionValue &&
                                    x.UserPermission.PermissionType == directoryPermissionsDto.Permission.PermissionType &&
                                    x.DirectoryPermission.DirectoryId == directoryPermissionsDto.DirectoryId)
                        .AnyAsync();

            if (!exists)
            {
                if (directoryPermissionsDto != null && directoryPermissionsDto.Permission != null)
                {
                    var userPermission = _mapper.Map<UserPermissions>(directoryPermissionsDto.Permission);
                    _context.UserPermissions.Add(userPermission);
                    _context.SaveChanges();

                    var directoryPermission = _mapper.Map<DirectoryPermission>(directoryPermissionsDto);
                    directoryPermission.PermissionId = userPermission.PermissionId;
                    _context.DirectoryPermissions.Add(directoryPermission);
                    _context.SaveChanges();
                    return new ApiResponse<DirectoryPermissionsDto>(directoryPermissionsDto);
                }

                return new ApiResponse<DirectoryPermissionsDto>(ResponseMessages.CouldNotSetDirectoryPermissions);
            }
            else
            {

                var userPermission = _mapper.Map<UserPermissions>(directoryPermissionsDto.Permission);
                _context.UserPermissions.Update(userPermission);
                _context.SaveChanges();

                var directoryPermission = _mapper.Map<DirectoryPermission>(directoryPermissionsDto);
                directoryPermission.PermissionId = userPermission.PermissionId;
                _context.DirectoryPermissions.Update(directoryPermission);
                return new ApiResponse<DirectoryPermissionsDto>(directoryPermissionsDto);
            }

        }
    }
}