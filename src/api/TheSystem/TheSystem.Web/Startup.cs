using Application.Abstractions;
using Application.Abstractions.Messaging;
using Application.AppUsers.ViewModels;
using Application.Services;
using Application.Services.Abstractions;
using Domain.Abstractions;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TheSystem.Web.Extensions;

namespace TheSystem.Web;

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
                .FromAssemblies(typeof(Infrastructure.AssemblyReference).Assembly, typeof(Application.AssemblyReference).Assembly)
                .AddClasses(filter => 
                {
                    filter.Where(t => t.IsAssignableTo(typeof(ICommand)) == false);
                    filter.Where(t => t.IsAssignableTo(typeof(ICommand<>)) == false);
                    filter.Where(t => t.IsAssignableTo(typeof(IViewModel)) == false);

                }, publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.AddMediatR(typeof(Application.AssemblyReference).Assembly);

        services.AddDbContext<ApplicationContext>(opt =>
        {
            opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });

        services.ProvideIdentity();
        services.AddAutoMapping();
        services.AddControllers();

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