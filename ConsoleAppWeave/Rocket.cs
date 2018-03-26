using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppWeave
{
	public class Rocket : IRocket
	{
		public string Name { get; set; }
		public string Model { get; set; }
		public int Launch(int delaySeconds)
		{
			Console.WriteLine(string.Format("Launching rocket in {0} seconds", delaySeconds));
			Thread.Sleep(1000 * delaySeconds);
			Console.WriteLine("Congratulations! You have successfully launched the rocket");
			return 1000;
		}
	}
}
