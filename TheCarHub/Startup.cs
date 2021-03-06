using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TheCarHub.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheCarHub.Repositories;
using AutoMapper;
using FluentValidation.AspNetCore;
using TheCarHub.Models.Validators;
using TheCarHub.Services;
using TheCarHub.Utilities;

namespace TheCarHub
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);

            services.AddSingleton<IFileUtility, FileUtility>();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AppReferential")));
                
                services.AddDbContext<AppIdentityDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AppIdentity"))
                );
            }
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppReferentialLocal")));
                
            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AppIdentityLocal"))
            );

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppIdentityDbContext>();
                
            services.AddTransient<ICarRepository, CarRepository>();
            services.AddTransient<IListingRepository, ListingRepository>();
            services.AddTransient<IMediaRepository, MediaRepository>();
            services.AddTransient<IStatusRepository, StatusRepository>();
            services.AddTransient<IRepairJobRepository, RepairJobRepository>();
            
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IListingService, ListingService>();
            services.AddScoped<IMediaService, MediaService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IRepairJobService, RepairJobService>();

            services.AddControllersWithViews()
                .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<ListingInputModelValidator>();
                        fv.ImplicitlyValidateChildProperties = true;
                        fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    }
                );

            services.AddAutoMapper(typeof(Startup));

            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this 
                // for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseDefaultFiles(new DefaultFilesOptions()
            {
                DefaultFileNames = new List<string> { "Index.html" } 
            });

            app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    "admin",
                    "Admin",
                    "Admin/{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages();
            });
        }
        
        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
            
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AppIdentityDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
