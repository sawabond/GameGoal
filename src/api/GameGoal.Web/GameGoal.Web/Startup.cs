using GameGoal.Data;
using GameGoal.Data.Interfaces;
using GameGoal.Web.Extensions;
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
            services.AddDbContext<ApplicationContext>(opt =>
            {
                opt.UseInMemoryDatabase("InMemory");
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

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
}