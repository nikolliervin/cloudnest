using AutoMapper;
using CloudNest.Api.Models.Dtos;
using CloudNest.Api.Models;
using Directory = CloudNest.Api.Models.Directory;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();

        CreateMap<Directory, DirectoryDto>()
                .ForMember(dest => dest.Deleted, opt => opt.MapFrom(src => src.Deleted));

        CreateMap<DirectoryDto, Directory>();
    }
}
