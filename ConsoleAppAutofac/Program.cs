using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAutofac
{
	class Program
	{
		static void Main(string[] args)
		{
			ContainerBuilder builder = new ContainerBuilder();
			builder.RegisterType<Log>().As<ILog>();
			builder.RegisterType<ProductService>().As<IProduct>();
			var container = builder.Build();
			var product1 = container.Resolve<IProduct>();
			var product2 = container.Resolve<IProduct>();
			Console.WriteLine(product1 == product2);
			product1.Run();
			Console.ReadLine();
		}
	}
}
