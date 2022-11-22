using Application.AppUsers.Commands.CreateUser;
using Application.AppUsers.ViewModels;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppUser, AppUserViewModel>();
        CreateMap<CreateUserCommand, AppUser>();
    }
}