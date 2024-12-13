using AutoMapper;
using CloudNest.Api.Helpers;
using CloudNest.Api.Interfaces;
using CloudNest.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CloudNest.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        private readonly IMapper _mapper;

        private readonly UserHelper _userHelper;
        public UserService(ApplicationDbContext dbContext, UserManager<User> userManager, IMapper mapper, UserHelper userHelper)
        {
            _context = dbContext;
            _userManager = userManager;
            _mapper = mapper;
            _userHelper = userHelper;
        }

        public async Task<ApiResponse<UpdateUserDto>> UpdateUserAsync(UpdateUserDto requestUser)
        {
            var currentUserId = _userHelper.GetCurrentUserId();

            var user = await _context.Users.FindAsync(currentUserId);

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
                    return new ApiResponse<UpdateUserDto>(_mapper.Map<UpdateUserDto>(user), ResponseMessages.UpdatedSuccesfully);
                }

                _context.SaveChanges();
                return new ApiResponse<UpdateUserDto>(_mapper.Map<UpdateUserDto>(user), ResponseMessages.UpdatedSuccesfully);
            }

            return new ApiResponse<UpdateUserDto>(ResponseMessages.CouldNotUpdate);
        }

        public async Task<ApiResponse<UpdateUserDto>> GetUserSettings()
        {
            var currentUserId = _userHelper.GetCurrentUserId();

            var user = await _context.Users.FindAsync(currentUserId);

            if (user == null)
            {
                return new ApiResponse<UpdateUserDto>(ResponseMessages.UserNotFound);
            }

            return new ApiResponse<UpdateUserDto>(_mapper.Map<UpdateUserDto>(user));

        }
    }
}