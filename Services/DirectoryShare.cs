using CloudNest.Api.Interfaces;
using CloudNest.Api.Models.Dtos;
using CloudNest.Api.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CloudNest.Api.Services
{
    public class DirectoryShareService : IDirectoryShareService
    {
        private readonly ApplicationDbContext _context;

        public DirectoryShareService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<bool>> ShareDirectoryAsync(DirectoryShareDto directoryShareDto)
        {
            var directory = await _context.Directories.FindAsync(directoryShareDto.DirectoryId);

            if (directory == null)
            {
                return new ApiResponse<bool>(ResponseMessages.DirectoryNotFound);
            }

            if (_context.DirectoryShares.Any(ds => ds.DirectoryId == directoryShareDto.DirectoryId && ds.UserId == directoryShareDto.UserId))
            {
                return new ApiResponse<bool>(ResponseMessages.DirectoryShareExists);
            }

            var DirectoryShares = new DirectoryShare
            {
                DirectoryId = directoryShareDto.DirectoryId,
                UserId = directoryShareDto.UserId
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
            {
                return new ApiResponse<bool>(ResponseMessages.DirectoryShareNotFound);
            }

            _context.DirectoryShares.Remove(DirectoryShares);
            await _context.SaveChangesAsync();

            return new ApiResponse<bool>(true, ResponseMessages.DirectoryShared);
        }
    }
}
