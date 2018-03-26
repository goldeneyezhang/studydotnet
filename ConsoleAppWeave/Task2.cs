using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWeave
{
	public class Task2 : ITask, ITransient
	{
		public IPerson person { get; set; }
		public void Run()
		{
			Console.WriteLine("属性注入");
		}
	}
}
