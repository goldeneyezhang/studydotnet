using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Hangfire;
using System.IO;
namespace WebApplicationAutofac
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			Console.WriteLine(Configuration.GetConnectionString("SqlConnection"));
		}

		public IConfiguration Configuration { get; }
		public IContainer ApplicationContainer { get; private set; }
		// This method gets called by the runtime. Use this method to add services to the container.
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("SqlConnection")));
			//services.AddHangfire();
			var builder = new ContainerBuilder();
			//注意写法
			builder.RegisterType<GuidTransientAppService>().As<IGuidTransientAppService>();
			builder.RegisterType<GuidScopedAppService>().As<IGuidScopedAppService>().InstancePerLifetimeScope();
			builder.RegisterType<GuidSingletonAppService>().As<IGuidSingletonAppService>().SingleInstance();
			builder.Populate(services);
			this.ApplicationContainer = builder.Build();
			GlobalConfiguration.Configuration.UseAutofacActivator(ApplicationContainer);
			return new AutofacServiceProvider(this.ApplicationContainer);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseMvc();
			app.UseHangfireDashboard();
			app.UseHangfireServer();
		}
	}
}
