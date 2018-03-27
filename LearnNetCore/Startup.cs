using Autofac;
using Autofac.Extensions.DependencyInjection;
using LearnNetCore.Constraint;
using LearnNetCore.Context;
using LearnNetCore.Controllers;
using LearnNetCore.Interfaces;
using LearnNetCore.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace LearnNetCore
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			//指定环境配置文件
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
			if (env.IsDevelopment())
			{
				builder.AddUserSecrets(new Guid().ToString());
			}
			builder.AddEnvironmentVariables();
			//内置变量
			builder.AddInMemoryCollection(new Dictionary<string, string>
			{
				{"username","Guest" }
			});
			Console.WriteLine("Added Memory Source. Sources: " + builder.Sources.Count);
			var args = Environment.GetCommandLineArgs().Skip(1).ToArray();
			builder.AddCommandLine(args);
			Console.WriteLine("Added Command Line Source. Sources: " + builder.Sources.Count);
			Configuration = builder.Build();
			Console.WriteLine(Configuration.GetConnectionString("DefaultConnection"));
			Console.WriteLine($"Hello,{Configuration["username"]}");
		}
		public IConfiguration Configuration { get; }
		/// <summary>
		/// 配置服务
		/// </summary>
		/// <param name="services"></param>
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{

			// Register the ConfigurationBuilder instance which MyOptions binds against.
			//services.AddTransient<IOperationTransient, Operation>();
			//services.AddScoped<IOperationScoped, Operation>();
			//services.AddSingleton<IOperationSingleton, Operation>();
			//services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));
			//services.AddTransient<OperationService, OperationService>();
			services.Configure<MyOptions>(Configuration);
			services.AddDirectoryBrowser();
			//Adds a default in-memory implementation of IDistributedCache
			services.AddDistributedMemoryCache();
			//绘画超时时间10秒
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromSeconds(10);
			});
			services.AddMemoryCache();

			//Add framework servcies.
			services.AddMvc(
				options =>
				{
					options.Conventions.Add(new RabbitConvention());
					options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
					options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
				}
				);
			//Add Autofac
			var builder = new ContainerBuilder();
			builder.Populate(services);
			RegisterType(builder);
			var container = builder.Build();

			return new AutofacServiceProvider(container);
		}
		/// <summary>
		/// autofac registerType注入
		/// </summary>
		/// <param name="builder"></param>
		private void RegisterType(ContainerBuilder builder)
		{
			builder.RegisterType<Operation>().As<IOperationTransient>();
			builder.RegisterType<Operation>().As<IOperationScoped>().InstancePerLifetimeScope();
			builder.RegisterType<Operation>().As<IOperationSingleton>().SingleInstance();
			builder.RegisterInstance(new Operation(Guid.Empty)).As<IOperationSingletonInstance>();
			builder.RegisterType<OperationService>();
			builder.RegisterType<RequestEntryCollectionMethod>().As<IRequestEntry>().InstancePerLifetimeScope();
			builder.RegisterType<BlogsService>().As<IBlogService>();
			builder.RegisterType<DataContext>();
		}

		/// <summary>
		/// 配置中间件
		/// </summary>
		/// <param name="app"></param>
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			//loggerFactory.AddConsole();
			loggerFactory.AddDebug();
			var testSwitch = new SourceSwitch("sourceSwitch", "Logging Sample");
			testSwitch.Level = SourceLevels.Warning;
			loggerFactory.AddTraceSource(testSwitch, new TextWriterTraceListener(writer: Console.Out));
			app.UseRequestIP();//自定义的middleware
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseSession();
			//set up custom content types-associating file extension to MIME type
			var provider = new FileExtensionContentTypeProvider();
			//add new mappings
			provider.Mappings[".myapp"] = "application/x-msdownload";
			provider.Mappings[".htm3"] = "text/html";
			provider.Mappings[".image"] = "image/png";
			//replace an existing mapping
			provider.Mappings[".rtf"] = "application/x-msdownload";
			//remove mp4 videos
			provider.Mappings.Remove(".mp4");
			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(
					Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images")),
				RequestPath = "/MyImages",
				ContentTypeProvider = provider
			});
			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(
					Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
				RequestPath = "/MyStaticFiles"
			});
			app.UseDirectoryBrowser(new DirectoryBrowserOptions()
			{
				FileProvider = new PhysicalFileProvider(
					Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images")),
				RequestPath = new PathString("/MyImages")
			});
			app.UseMvc(routes =>
			{
				routes.MapRoute(name: "areaRoute",
					template: "{area:exists}/{controller=Home}/{action=Index}");
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
			//app.UseMvcWithDefaultRoute();
			ConfigureMapping(app);
			ConfigurationMapWhen(app);
		}
		/// <summary>
		/// 配置map
		/// </summary>
		/// <param name="app"></param>
		public void ConfigureMapping(IApplicationBuilder app)
		{
			app.Map("/maptest", HandleMapTest);
		}
		public void ConfigurationMapWhen(IApplicationBuilder app)
		{
			app.MapWhen(context =>
			{
				return context.Request.Query.ContainsKey("branch");
			}, HandleBranch);
		}
		private static void HandleMapTest(IApplicationBuilder app)
		{
			app.Run(async context =>
			{
				await context.Response.WriteAsync("Map Test Successful");
			});
		}
		private static void HandleBranch(IApplicationBuilder app)
		{
			app.Run(async context =>
			{
				await context.Response.WriteAsync("Branch used.");
			});
		}
	}
}
