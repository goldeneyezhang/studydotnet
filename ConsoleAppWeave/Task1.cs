using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWeave
{
	/// <summary>
	/// 构造函数注入
	/// </summary>
	public class Task1:ITransient, ITask
	{
		private IPerson _person;
		public Task1(IPerson person)
		{
			this._person = person;
		}
		public void Run()
		{
			Console.WriteLine("构造函数注入");
		}
	}
}
