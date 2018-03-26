using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject1
{
	public interface ICalculator
	{
		int Add(int a, int b);
		string Mode { get; set; }
		event EventHandler PoweringUp;
		string Bar(int a, string b);
	}
}
