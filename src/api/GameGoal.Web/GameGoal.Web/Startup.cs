using Domain.Abstractions;
using GameGoal.Web.Extensions;
using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameGoal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;

            services.AddControllers()
                .AddApplicationPart(presentationAssembly);

            services
                .Scan(selector => selector
                    .FromAssemblies(typeof(Infrastructure.AssemblyReference).Assembly)
                    .AddClasses(publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

            services.AddMediatR(typeof(Application.AssemblyReference).Assembly);

            services.AddDbContext<ApplicationContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.ProvideIdentity();
            services.AddAutoMapping();
            services.AddControllers();

            services.AddScoped<ISeeder, Seeder>();

            services.AddBearerAuthentication(Configuration);

            services.AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityServer v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}