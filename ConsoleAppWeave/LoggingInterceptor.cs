using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWeave
{
	public class LoggingInterceptor : IInterceptor
	{
		public void Intercept(IInvocation invocation)
		{
			var methodName = invocation.Method.Name;
			try
			{
				Console.WriteLine(string.Format("Entered Method:{0}, Arguments: {1}", methodName, string.Join(",", invocation.Arguments)));
				invocation.Proceed();
				Console.WriteLine(string.Format("Sucessfully executed method:{0}", methodName));
				invocation.ReturnValue = 50;
			}
			catch (Exception e)
			{
				Console.WriteLine(string.Format("Method:{0}, Exception:{1}", methodName, e.Message));
				throw;
			}
			finally
			{
				Console.WriteLine(string.Format("Exiting Method:{0}", methodName));
			}
		}
	}
}
