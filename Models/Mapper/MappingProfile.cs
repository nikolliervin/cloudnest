using AutoMapper;
using CloudNest.Api.Models.Dtos;
using CloudNest.Api.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>();
    }
}
