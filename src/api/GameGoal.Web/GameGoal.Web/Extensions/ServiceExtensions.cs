using AutoMapper;
using GameGoal.Data;
using GameGoal.Data.Entities;
using GameGoal.Web.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace GameGoal.Web.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ProvideIdentity(this IServiceCollection services)
        {
            //services.AddScoped<ITokenService, TokenService>();

            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<AppRole>()
                .AddRoleManager<RoleManager<AppRole>>()
                .AddSignInManager<SignInManager<AppUser>>()
                .AddRoleValidator<RoleValidator<AppRole>>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddAuthentication();

            return services;
        }

        public static IServiceCollection AddAutoMapping(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            services.AddSingleton(mapperConfig.CreateMapper() as IMapper);

            return services;
        }

        //public static IServiceCollection AddBearerAuthentication(this IServiceCollection services)
        //{
        //    var jwtOptions = services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>().Value;

        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(jwtOptions.TokenKey.ToByteArray()),
        //        ValidateIssuer = false,
        //        ValidateAudience = false,
        //        RequireExpirationTime = false,
        //        ValidateLifetime = true
        //    };

        //    services.AddSingleton(tokenValidationParameters);

        //    services
        //        .AddAuthentication(options =>
        //        {
        //            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //        })
        //        .AddJwtBearer(options =>
        //        {
        //            options.SaveToken = true;
        //            options.TokenValidationParameters = tokenValidationParameters;
        //        });

        //    return services;
        //}

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GameGoal", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Authorization using Bearer scheme 'Bearer <token>'",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }
    }
}