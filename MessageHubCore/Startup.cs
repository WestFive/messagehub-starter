using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using MessageHubCore.Hubs;

namespace MessageHubCore
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
            services.AddMvc();
            services.AddSignalR();
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyHeader()
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()));
            services.AddDirectoryBrowser();//ÎÄ¼þä¯ÀÀ
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");
            app.UseFileServer();
            app.UseSignalR(routes =>
            {
                routes.MapHub<MessageHub>("/MessageHub");
            });


            var dir = new DirectoryBrowserOptions();
            dir.FileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory);
            app.UseDirectoryBrowser(dir);
            var staticfile = new StaticFileOptions();
            staticfile.FileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory);
            app.UseStaticFiles(staticfile);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
