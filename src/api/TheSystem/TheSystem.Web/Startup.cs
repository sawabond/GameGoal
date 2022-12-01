using Application.Abstractions;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Presentation.Extensions;

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

        services
            .AddControllers()
            .ExcludeRecursiveNesting()
            .AddApplicationPart(presentationAssembly);

        services
            .Scan(selector => selector
                .FromAssemblies(typeof(Infrastructure.AssemblyReference).Assembly, typeof(Application.AssemblyReference).Assembly)
                .AddClasses(filter => 
                {
                    filter.Where(t => t.IsAssignableTo(typeof(IViewModel)) == false);
                    filter.Where(t => t.Name.EndsWith("Command") == false);
                    filter.Where(t => t.Name.EndsWith("Query") == false);
                }, publicOnly: false)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
            });
        });

        services.AddMediatR(typeof(Application.AssemblyReference).Assembly);

        services.AddDbContext<ApplicationContext>(opt =>
        {
            opt
            .UseLazyLoadingProxies()
            .UseInMemoryDatabase("DB");
            //.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
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

        app.UseCors();

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