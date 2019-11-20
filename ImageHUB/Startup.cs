using ImageHUB.Repositories;
using ImageHUB.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ImageHUB
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
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDirectoryBrowser();

            services.AddSingleton<IDatabaseContext, DatabaseContext>();
            services.AddSingleton<IRepository, Repository>();
            services.AddScoped<IImageStorage, ImageStorage>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IProfileService, ProfileService>();

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
            }).AddCookie();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

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

            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseMvc(endpoints =>
            {
                endpoints.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
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
