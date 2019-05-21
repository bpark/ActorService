using ActorService.AppServices;
using ActorService.Model;
using ActorService.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ActorService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string AllowedOrigins = "AllowedOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<ModelContext>(options =>
            {
                options.UseMySQL(Configuration["ConnectionString"]);
            });

            // register the repository
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IZoneRepository, ZoneRepository>();
            services.AddSingleton<IActorFactory, ActorFactory>();

            services.AddScoped<GetActorListQueryHandler>();
            services.AddScoped<GetActorQueryHandler>();
            services.AddScoped<CreateActorCommandHandler>();

            services.AddScoped<CreateZoneCommandHandler>();
            services.AddScoped<GetZoneListQueryHandler>();
            
            services.AddCors(options =>
            {
                options.AddPolicy(AllowedOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "http://localhost:5002");
                    });
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(AllowedOrigins);
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}