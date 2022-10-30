﻿using AutoMapper;
using GameGoal.Data.Entities;
using GameGoal.Web.RequestModels.Goal;
using GameGoal.Web.RequestModels.User;
using GameGoal.Web.ViewModels.Goal;
using GameGoal.Web.ViewModels.User;

namespace GameGoal.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // AppUser

            CreateMap<AppUser, UserViewModel>();
            CreateMap<AppUser, UserHormonalStateViewModel>();

            CreateMap<RegisterUserRequestModel, AppUser>();
            CreateMap<LoginUserRequestModel, AppUser>();

            // Goal

            CreateMap<CreateGoalRequestModel, Goal>();
            CreateMap<Goal, GoalViewModel>();
        }
    }
}