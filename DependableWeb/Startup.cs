using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControllerDI.Services;
using DependableWeb.Interfaces;
using DependableWebCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DependableWeb
{
    public class Startup
    {
		private DependableServiceProvider dependableServiceProvider;
		public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddMvc();
			//services.AddTransient<IDateTime, SystemDateTime>();
			dependableServiceProvider = new DependableServiceProvider();
			dependableServiceProvider.Register<IDateTime, SystemDateTime>();
			dependableServiceProvider.Build(services);

			//dependableServiceProvider.Register<Interfaces.IDateTime, SystemDateTime>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
			if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
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
            });
        }
    }
}
