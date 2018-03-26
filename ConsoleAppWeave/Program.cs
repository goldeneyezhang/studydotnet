using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWeave
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var container = new WindsorContainer())
			{
				container.Install(new AssmInstaller());
				var person = container.Resolve<IPerson>();
				person.Say();
				var task = container.Resolve<ITask>();
				var task2= container.ResolveAll<ITask>().FirstOrDefault(x=>x.GetType()==typeof(Task2));
				
				task.Run();
				task2.Run();
			}
				DependencyResolver.Initialize();

			//resolve the type:Rocket
			var rocket = DependencyResolver.For<IRocket>();

			//method call
			try
			{
				var result=rocket.Launch(5);
				Console.WriteLine(result);
			}
			catch (Exception ex)
			{

			}
			System.Console.ReadKey();
		}
	}
}
