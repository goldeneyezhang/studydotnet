using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleAppString
{
	public class ThreadIdEnricher : ILogEventEnricher
	{
		public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
		{
			logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(
			   "ThreadId", Thread.CurrentThread.ManagedThreadId));
		}
	}
}
