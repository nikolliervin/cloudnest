using AutoMapper;
using CloudNest.Api.Dtos;
using CloudNest.Api.Helpers;
using CloudNest.Api.Interfaces;
using CloudNest.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace CloudNest.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;


        private readonly UserHelper _userHelper;
        public UserService(ApplicationDbContext dbContext, UserManager<User> userManager, UserHelper userHelper)
        {
            _context = dbContext;
            _userManager = userManager;
            _userHelper = userHelper;
        }

        public async Task<ApiResponse<UpdateUserDto>> UpdateUserAsync(UpdateUserDto requestUser)
        {
            var currentUserId = _userHelper.GetCurrentUserId();

            var user = _context.Users.Where(u => u.Id == currentUserId.ToString()).FirstOrDefault();

            if (user == null)
            {
                return new ApiResponse<UpdateUserDto>(ResponseMessages.UserNotFound);
            }

            user.UserName = requestUser.Username ?? user.UserName;
            user.NormalizedUserName = requestUser.Username ?? user.UserName.ToUpper();
            user.Email = requestUser.Email ?? user.Email;
            user.NormalizedEmail = requestUser.Email ?? user.Email.ToUpper();

            _context.Users.Update(user);
            var res = await _context.SaveChangesAsync();
            if (res > 0)
            {

                if (requestUser.Password != null)
                {
                    var passRes = _userManager.ChangePasswordAsync(user, requestUser.OldPassword, requestUser.Password);
                    if (passRes.Result.Succeeded)
                        return new ApiResponse<UpdateUserDto>(new UpdateUserDto { Username = user.UserName, Email = user.Email, UserId = new Guid(user.Id) }, ResponseMessages.UpdatedSuccesfully);
                    return new ApiResponse<UpdateUserDto>(ResponseMessages.CouldNotUpdate);
                }

                _context.SaveChanges();
                return new ApiResponse<UpdateUserDto>(new UpdateUserDto { Username = user.UserName, Email = user.Email, UserId = new Guid(user.Id) }, ResponseMessages.UpdatedSuccesfully);
            }

            return new ApiResponse<UpdateUserDto>(ResponseMessages.CouldNotUpdate);
        }

        public async Task<ApiResponse<UpdateUserDto>> GetUserSettings()
        {
            var currentUserId = _userHelper.GetCurrentUserId();

            var user = await _context.Users.Where(u => u.Id == currentUserId.ToString()).FirstOrDefaultAsync();

            if (user == null)
            {
                return new ApiResponse<UpdateUserDto>(ResponseMessages.UserNotFound);
            }

            return new ApiResponse<UpdateUserDto>(new UpdateUserDto { Username = user.UserName, Email = user.Email, UserId = new Guid(user.Id) });

        }

        public async Task<ApiResponse<List<StorageInfoDto>>> GetStorageData()
        {
            var storage = _userHelper.GetStorageInfo();

            if (storage != null && storage.ToList().Count > 0)
            {
                return new ApiResponse<List<StorageInfoDto>>(storage);
            }

            return new ApiResponse<List<StorageInfoDto>>(ResponseMessages.CouldNotFindStorage);

        }
    }
}