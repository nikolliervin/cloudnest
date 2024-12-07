using CloudNest.Api.Interfaces;
using CloudNest.Api.Models.Dtos;
using CloudNest.Api.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Directory = CloudNest.Api.Models.Directory;
using System.Security.Cryptography;
using System.Collections.ObjectModel;
using AutoMapper;

namespace CloudNest.Api.Services
{
    public class DirectoryShareService : IDirectoryShareService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public DirectoryShareService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<bool>> ShareDirectoryAsync(DirectoryShareDto directoryShareDto)
        {
            var directory = await _context.Directories.FindAsync(directoryShareDto.DirectoryId);

            if (directory == null)
                return new ApiResponse<bool>(ResponseMessages.DirectoryNotFound);

            if (_context.DirectoryShares.Any(ds => ds.DirectoryId == directoryShareDto.DirectoryId && ds.UserId == directoryShareDto.UserId))
                return new ApiResponse<bool>(ResponseMessages.DirectoryShareExists);


            var DirectoryShares = new DirectoryShare
            {
                DirectoryId = directoryShareDto.DirectoryId,
                UserId = directoryShareDto.UserId,
                ExpiryDate = directoryShareDto.ExpiryDate,
            };

            await _context.DirectoryShares.AddAsync(DirectoryShares);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessages.DirectoryShared);
        }

        public async Task<ApiResponse<bool>> RemoveShareDirectoryAsync(DirectoryShareDto directoryShareDto)
        {
            var DirectoryShares = await _context.DirectoryShares
                .FirstOrDefaultAsync(ds => ds.DirectoryId == directoryShareDto.DirectoryId && ds.UserId == directoryShareDto.UserId);

            if (DirectoryShares == null)
                return new ApiResponse<bool>(ResponseMessages.DirectoryShareNotFound);

            _context.DirectoryShares.Remove(DirectoryShares);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessages.DirectoryShared);
        }

        public async Task<ApiResponse<List<DirectoryDto>>> GetSharedDirectoriesAsync(Guid userId)
        {
            var sharedDirectories = await _context.Directories
                .Where(d => !d.Deleted)
                .Join(_context.DirectoryShares,
                    d => d.DirectoryId,
                    ds => ds.DirectoryId,
                    (d, ds) => new { d, ds })
                .Where(x => x.ds.UserId == userId && !x.ds.DeletedAt.HasValue && x.ds.ExpiryDate < DateTime.Now)
                .Select(x => x.d)
                .ToListAsync();
            return new ApiResponse<List<DirectoryDto>>(_mapper.Map<List<DirectoryDto>>(sharedDirectories));

        }
    }
}
