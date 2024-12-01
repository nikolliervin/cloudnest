using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CloudNest.Api.Helpers;
using CloudNest.Api.Interfaces;
using CloudNest.Api.Models.Dtos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Directory = CloudNest.Api.Models.Directory;

namespace CloudNest.Api.Services
{

    public class DirectoryService : IDirectoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly string? _baseDirectoryPath;

        public DirectoryService(ApplicationDbContext context, IMapper mapper, UserHelper userHelper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _userHelper = userHelper;
            _configuration = configuration;
            _baseDirectoryPath = _configuration["FileSystemBasePath"];
        }

        public async Task<ApiResponse<DirectoryDto>> CreateDirectoryAsync(DirectoryDto directoryDto)
        {
            var directory = _mapper.Map<Directory>(directoryDto);

            _context.Directories.Add(directory);
            await _context.SaveChangesAsync();
            var directoryCreated = DirectoryHelper.CreateDirectoryInFileSystem(directoryDto, _baseDirectoryPath);
            if(directoryCreated){
                await _context.SaveChangesAsync();
                var responseDto = _mapper.Map<DirectoryDto>(directory);
                return new ApiResponse<DirectoryDto>(responseDto, ResponseMessages.DirectoryCreated);
            }

            return new ApiResponse<DirectoryDto>(ResponseMessages.DirectoryExists);
        }

        public async Task<ApiResponse<DirectoryDto>> UpdateDirectoryAsync(DirectoryDto directoryDto)
        {
            var directory = await _context.Directories.FindAsync(directoryDto.DirectoryId);
            if (directory == null)
            {
                return new ApiResponse<DirectoryDto>(ResponseMessages.DirectoryNotFound);
            }

            _mapper.Map(directoryDto, directory);

            directory.UpdatedAt = DateTime.UtcNow;
            directory.UpdatedBy = directoryDto.UserId;

            _context.Directories.Update(directory);
            await _context.SaveChangesAsync();

            var responseDto = _mapper.Map<DirectoryDto>(directory);

            return new ApiResponse<DirectoryDto>(responseDto, ResponseMessages.DirectoryUpdated);
        }

        public async Task<ApiResponse<bool>> DeleteDirectoryAsync(Guid id)
        {
            var directory = await _context.Directories.FindAsync(id);
            if (directory == null)
            {
                return new ApiResponse<bool>(ResponseMessages.DirectoryNotFound);
            }

            directory.Deleted = true;
            directory.DeletedAt = DateTime.UtcNow;
            directory.DeletedBy = directory.UserId;

            _context.Directories.Update(directory);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessages.DirectoryDeleted);
        }

        public async Task<ApiResponse<DirectoryDto>> GetDirectoryAsync(Guid id)
        {
            var directory = await _context.Directories
                .Where(d => d.DirectoryId == id && !d.Deleted)
                .FirstOrDefaultAsync();

            if (directory == null)
            {
                return new ApiResponse<DirectoryDto>(ResponseMessages.DirectoryNotFound);
            }

            var responseDto = _mapper.Map<DirectoryDto>(directory);
            return new ApiResponse<DirectoryDto>(responseDto);
        }

        public async Task<ApiResponse<IEnumerable<DirectoryDto>>> GetUserDirectoriesAsync()
        {
            var userId = _userHelper.GetCurrentUserId();

            var directories = await _context.Directories
                .Where(d => d.UserId == userId && !d.Deleted)
                .ToListAsync();

            var directoryDtos = _mapper.Map<IEnumerable<DirectoryDto>>(directories);

            return new ApiResponse<IEnumerable<DirectoryDto>>(directoryDtos);
        }
    }


}

