using AutoMapper;
using CloudNest.Api.Models.Dtos;
using CloudNest.Api.Models;
using Directory = CloudNest.Api.Models.Directory;
using Microsoft.AspNetCore.Identity;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<User, UserDto>();
    CreateMap<IdentityUser, RegisterDto>()
      .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
      .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

    CreateMap<Directory, DirectoryDto>()
            .ForMember(dest => dest.Deleted, opt => opt.MapFrom(src => src.Deleted));

    CreateMap<DirectoryDto, Directory>();
    CreateMap<DirectoryPermissionsDto, DirectoryPermission>();
    CreateMap<UserPermissionsDto, UserPermissions>();

    CreateMap<DirectoryPermission, DirectoryPermissionsDto>();
    CreateMap<UserPermissions, UserPermissionsDto>();

  }
}
