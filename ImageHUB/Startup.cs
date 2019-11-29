using ImageHUB.Repositories;
using ImageHUB.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.IO;
using System.Threading.Tasks;

namespace imagehubsample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
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

            //var dbPath = "Server=127.0.0.1;Port=3306;Database=imghub;User Id=migrator; Password=nincs";
            //var dbPath = "Server=127.0.0.1;Port=3306;Database=imghub;User Id=migrator; Password=migrationpwd";
            var dbPath = "Server=127.0.0.1;Port=55615;Database=localdb;User Id=azure; Password=6#vWHD_$;";
            services.AddDbContextPool<DatabaseContext>(options =>
                options.UseMySql(dbPath, mySqlOptions =>
                {
                    mySqlOptions.ServerVersion(new Version(5, 7, 9), ServerType.MySql);
                }
            ));

            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IImageStorage, ImageStorage>();
            services.AddScoped<IPostService, PostService>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "app/build";
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
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            var path = Path.Combine(Directory.GetCurrentDirectory(), this.Configuration["ImageSavePath"]);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            
            var pfp = new PhysicalFileProvider(path);
            app.UseStaticFiles(new StaticFileOptions{
                FileProvider = pfp,
                RequestPath = "/img"
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions{
                FileProvider = pfp,
                RequestPath = "/img"
            });

            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "app";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
