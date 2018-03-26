using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWeave
{
	public class Person:IPerson,ITransient
	{
		public void Say()
		{
			Console.WriteLine("Person's say method is Called");
		}

		
	}
}
