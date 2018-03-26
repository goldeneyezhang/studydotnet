using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWeave
{
	public class ComponentRegistration : IRegistration
	{
		public void Register(IKernelInternal kernel)
		{
			kernel.Register(
				Component.For<LoggingInterceptor>()
					.ImplementedBy<LoggingInterceptor>());

			kernel.Register(
				Component.For<IRocket>()
						 .ImplementedBy<Rocket>()
						 .Interceptors(InterceptorReference.ForType<LoggingInterceptor>()).Anywhere);
		}
	}
}
