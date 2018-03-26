using Autofac;
using Autofac.Integration.Mvc;
using ConsoleAppAutofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication1
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			InitialAutofac();
		}
		private void InitialAutofac()
		{
			var builder = new ContainerBuilder();

			// Register your MVC controllers. (MvcApplication is the name of
			// the class in Global.asax.)
			builder.RegisterControllers(typeof(MvcApplication).Assembly);

			// OPTIONAL: Register model binders that require DI.
			builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
			builder.RegisterModelBinderProvider();

			// OPTIONAL: Register web abstractions like HttpContextBase.
			builder.RegisterModule<AutofacWebTypesModule>();

			// OPTIONAL: Enable property injection in view pages.
			builder.RegisterSource(new ViewRegistrationSource());

			// OPTIONAL: Enable property injection into action filters.
			builder.RegisterFilterProvider();

			// OPTIONAL: Enable action method parameter injection (RARE).
			//builder.InjectActionInvoker();
			//自动注入
			var singletonType = typeof(ISingleton);
			var perDependency = typeof(IPerDependency);
			builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(t => singletonType.IsAssignableFrom(t) && t != singletonType).AsImplementedInterfaces().SingleInstance();
			builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies()).Where(t => perDependency.IsAssignableFrom(t) && t != perDependency).AsImplementedInterfaces();
			// Set the dependency resolver to be Autofac.
			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}
	}
}
