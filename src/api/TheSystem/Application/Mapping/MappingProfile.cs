using Application.AchievementSystems.Commands.CreateAchievementSystem;
using Application.AchievementSystems.ViewModels;
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

        CreateMap<CreateAchievementSystemCommand, AchievementSystem>();
        CreateMap<AchievementSystem, AchievementSystemViewModel>();
    }
}