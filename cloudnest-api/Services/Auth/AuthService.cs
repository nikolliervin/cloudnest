using Microsoft.AspNetCore.Identity;
using CloudNest.Api.Interfaces;
using Microsoft.Extensions.Configuration;
using CloudNest.Api.Models.Dtos;
using CloudNest.Api.Helpers;
using AutoMapper;
using Azure;
using CloudNest.Api.Models;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    private readonly JwtTokenHelper _jwtTokenHelper;

    private readonly IMapper _mapper;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration, ApplicationDbContext context, IMapper mapper, JwtTokenHelper _jwtHelper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _context = context;
        _mapper = mapper;
        _jwtTokenHelper = _jwtHelper;
    }

    public async Task<ApiResponse<RegisterDto>> RegisterAsync(RegisterDto registerDto)
    {
        if (registerDto.Password != registerDto.ConfirmPassword)
        {
            return new ApiResponse<RegisterDto>(ResponseMessages.PasswordsDoNotMatch);
        }

        var user = new User { UserName = registerDto.Username, Email = registerDto.Email };
        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            return new ApiResponse<RegisterDto>(_mapper.Map<RegisterDto>(user), ResponseMessages.UserCreatedSuccessfully);
        }
        return new ApiResponse<RegisterDto>(ResponseMessages.CouldNotCreateUser);
    }

    public async Task<ApiResponse<JwtLogin>> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user == null)
        {
            return new ApiResponse<JwtLogin>(ResponseMessages.InvalidCredentials);
        }

        var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
        if (result.Succeeded)
        {
            var token = _jwtTokenHelper.GenerateToken(user);
            return new ApiResponse<JwtLogin>(token);
        }

        return new ApiResponse<JwtLogin>(ResponseMessages.InvalidCredentials);
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }


}

