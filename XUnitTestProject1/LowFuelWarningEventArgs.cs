using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestProject1
{
    public class LowFuelWarningEventArgs:EventArgs
    {
		public int PercentLeft { get; }
		public LowFuelWarningEventArgs(int percentLeft)
		{
			PercentLeft = percentLeft;
		}
    }
	public class FuelManagement
	{
		public event EventHandler<LowFuelWarningEventArgs> LowFuelDetected;
		public void DoSomething()
		{
			LowFuelDetected?.Invoke(this, new LowFuelWarningEventArgs(15));
		}
	}
	
}
