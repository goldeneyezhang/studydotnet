using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace XUnitTestProject1
{
    public class CommandRepeater
    {
		ICommand command;
		int numberOfTimesToCall;
		public CommandRepeater(ICommand command,int numberOfTimesToCall)
		{
			this.command = command;
			this.numberOfTimesToCall = numberOfTimesToCall;
		}
		public void Execute()
		{
			for (var i = 0; i < numberOfTimesToCall; i++)
				command.Execute(1);
		}
    }
}
