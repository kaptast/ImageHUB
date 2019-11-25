using ImageHUB.Repositories;
using ImageHUB.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using System;

namespace ImageHUB
{
    public class Startup
    {
        private readonly ILogger<Startup> logger;
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            this.logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDirectoryBrowser();

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
             .AddFacebook(options =>
             {
                 options.AppId = this.Configuration["Facebook:AppId"];
                 options.AppSecret = this.Configuration["Facebook:Secret"];
             })
             .AddCookie(options =>
             {
                 options.Events.OnRedirectToLogin = context =>
                 {
                     context.Response.StatusCode = 401;
                     return Task.CompletedTask;
                 };
             });


            var dbPath = "Server=127.0.0.1;Port=49250;Database=localdb;User Id=azure; Password=6#vWHD_$;";
            logger.LogInformation("Database connection string: {0}", dbPath);
            services.AddDbContextPool<IDatabaseContext, DatabaseContext>(options =>
                options.UseMySql(dbPath, mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new Version(5, 7, 9), ServerType.MySql);
                }
            ));
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IImageStorage, ImageStorage>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IProfileService, ProfileService>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            var path = Path.Combine(Directory.GetCurrentDirectory(), this.Configuration["ImageSavePath"]);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var pfp = new PhysicalFileProvider(path);

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = pfp,
                RequestPath = "/img"
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = pfp,
                RequestPath = "/img"
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
