using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWeave
{
	public class AssmInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(Classes.FromThisAssembly()//选择assembly
				.IncludeNonPublicTypes() //约束type
				.BasedOn<ITransient>()  //约束type
				.WithService.DefaultInterfaces()//匹配类型
				.LifestyleTransient()//注册生命周期
				);
			container.Register();
		}
	}
}
