using AutoMapper;
using GameGoal.Data.Entities;
using GameGoal.Web.RequestModels;
using GameGoal.Web.ViewModels;

namespace GameGoal.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, UserViewModel>();

            CreateMap<RegisterUserRequestModel, AppUser>();
            CreateMap<LoginUserRequestModel, AppUser>();
        }
    }
}