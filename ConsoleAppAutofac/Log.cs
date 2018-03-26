using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAutofac
{

	public class Log : ILog
	{
		public void SaveLog(string message)
		{
			Console.WriteLine($"Log={message}");
		}
	}
}
