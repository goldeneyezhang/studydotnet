using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppTask
{
	class Program
	{
		private static readonly Dictionary<string, string> dicSeqText = new Dictionary<string, string>()
		{
			 {"Equal1", "航班起飛前"},
			{"Equal2", "航班起飛後"},
			{"Greater1", "航班起飛前{0}小時或之前"},
			{"Greater2", "航班起飛前{0}小時內"},
			{"Less1","航班起飛後{0}小時內"},
			{"Less2","航班起飛後{0}小時或之後"}
		};
		private static string FormatSeqText(decimal time, int seqNo)
		{
			if (decimal.Zero.Equals(time))
			{
				return dicSeqText["Equal" + seqNo];
			}

			var key = (decimal.Zero < time ? "Greater" : "Less") + seqNo;
			dicSeqText[key] = string.Format(dicSeqText[key], Math.Abs(time));

			return dicSeqText[key];
		}
		static void Main(string[] args)
		{
			var text = FormatSeqText(12, 1);
			Console.WriteLine(text);
			var text2 = FormatSeqText(12, 2);
			Console.WriteLine(text2);
			Stopwatch sw = new Stopwatch();
			sw.Start();
			Task t = AsynchronousProcessing();
			t.Wait();
			sw.Stop();
			Console.WriteLine(sw.ElapsedMilliseconds / 1000);
			Console.Read();
		}
		async static Task AsynchronousProcessing()
		{
			Task<string> t1 = GetInfoAsync("Task 1", 3);
			Task<string> t2 = GetInfoAsync("Task 2", 5);
			string[] results = await Task.WhenAll(t1, t2);
			foreach (string result in results)
			{
				Console.WriteLine(result);
			}
			Console.WriteLine("Hello World!");
		}
		async static Task<string> GetInfoAsync(string name, int seconds)
		{
			await Task.Delay(TimeSpan.FromSeconds(seconds));
			//await Task.Run(() => Thread.Sleep(TimeSpan.FromSeconds(seconds)));  
			return string.Format("Task {0} is running on a thread id {1}. Is thread pool thread: {2}",
			name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
		}
	}
}
