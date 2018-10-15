using log4net.Config;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace StudyCode
{
	class Program
	{
		private static int _result;
		/// <summary>
		/// Base64解密
		/// </summary>
		/// <param name="codeName">解密采用的编码方式，注意和加密时采用的方式一致</param>
		/// <param name="result">待解密的密文</param>
		/// <returns>解密后的字符串</returns>
		public static string DecodeBase64(Encoding encode, string result)
		{
			string decode = "";
			byte[] bytes = Convert.FromBase64String(result);
			try
			{
				decode = encode.GetString(bytes);
			}
			catch
			{
				decode = result;
			}
			return decode;
		}

		static void LongCalc(int n)
		{
			// 也没什么用，只是模拟耗时而已
			Thread.Sleep(n * 1000);
		}
		static void Main(string[] args)
		{
			Console.WriteLine(GzipUtil.GZipDecompressString("GAAAAHice9Yx8dmM9S6pJYmZOU/7179o3vt0X8fLqfsBtrEPMw=="));
			Stopwatch watch = new Stopwatch();
			watch.Start();
			Parallel.For(1, 4, LongCalc);
			watch.Stop();
			Console.WriteLine(watch.ElapsedMilliseconds);
			var bag = new ConcurrentBag<string>();

			var t1 = Task.Factory.StartNew(() =>
			{
				bag.Add("线程1: 1");
				Thread.Sleep(1000);
				bag.Add("线程1: 2");

				foreach (var str in bag)
					Console.WriteLine(str);
			});

			var t2 = Task.Factory.StartNew(() =>
			{
				bag.Add("线程2: 1");
				Thread.Sleep(2000);

				string str;
				bag.TryTake(out str);
				Console.WriteLine("线程2取出：" + str);
			});

			Task.WaitAll(t1, t2);
			//LogRequest log = new LogRequest("test", "testdetail", "source", "DotNet.Core", "INFO");
			//LogRequest[] LogList = new LogRequest[]{ log};
			//string json= "[{\"ThreadID\": 125,\"ThreadName\": \"yibozhangbbb\",\"ProcessID\": 19788,\"ProcessName\": \"19788@DST58268\",\"Message\": \"test messagexx\",\"AppDomainName\": \"test app namexx\",\"DetailStr\": \"test detail str\",\"CreateTime\": \"2018-01-11 18:50:04.856+08:00\",\"MachineName\": \"DST63522\",\"Source\": \"Rest.Service8888\",\"SystemCode\": \"DotNet.Core\",\"IPAddress\": \"10.32.230.2\",\"TagDict\": {\"LevelName\": \"INFO\",\"Level\": 20000}}]";
			//var logv = JsonConvert.DeserializeObject<LogRequest[]>(json);
			//var result=HttpClientUtil.PostAsJson<bool, LogRequest[]>("http://log.api.dev.corp.com/DebugLog/AddLog", logv, 5000).Result;
			//Thread.Sleep(1000);
			//var result2 = HttpClientUtil.PostAsJson<bool, LogRequest[]>("http://log.api.dev.corp.com/DebugLog/AddLog", LogList, 5000).Result;
			//Console.WriteLine(result);
			//ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
			//IDatabase db = redis.GetDatabase();
			//string value = "name";
			//db.StringSet("mykey", value);
			//Console.WriteLine(db.StringGet("mykey"));
			//while(true)
			//{
			//	Task[] _tasks = new Task[10];
			//	int i = 0;
			//	for(i=0;i<_tasks.Length;i++)
			//	{
			//		_tasks[i] = Task.Factory.StartNew((num) =>
			//		{
			//			var taskid = (int)num;
			//			Work(taskid);
			//		},i);
			//	}
			//	Task.WaitAll(_tasks);
			//	Console.WriteLine(_result);
			//	Console.ReadKey();
			//}
			Random r1 = new Random((int)DateTime.Now.Ticks / 10000);
			Random r2 = new Random((int)DateTime.Now.Ticks / 10000 - 100);
			Flight[] flights1 = new Flight[10000];
			int[] array1 = new int[10000];
			int[] array2 = new int[10000];
			for (int i = 0; i < 10000; i++)
			{
				array1[i] = r1.Next(1, 50000);
				array2[i] = r2.Next(1, 50000);
				flights1[i] = new Flight() { CityNo = array2[i] };
			}
			var a1 = array1.Distinct().ToArray();
			var a2 = array2.Distinct().ToArray();
			var f1 = flights1.Distinct(new FlightCompare()).ToArray();
			DoNotUserLinq(a1, a2);
			UserLinq(a1, a2);
			DoNotUserLinqF(a1, f1);
			UserLinqF(a1, f1);
			Console.Read();
		}
		//线程调用方法
		private static void Work(int TaskID)
		{
			for (int i = 0; i < 10; i++)
			{
				//_result++;
				Interlocked.Increment(ref _result);
			}
		}
		/// <summary>
		/// 双重循环
		/// </summary>
		/// <param name="array1"></param>
		/// <param name="array2"></param>
		private static void DoNotUserLinq(int[] array1, int[] array2)
		{
			List<int> result = new List<int>();
			var sw = Stopwatch.StartNew();
			foreach (int n1 in array1)
			{
				foreach (int n2 in array2)
				{
					if (n1 == n2)
					{
						result.Add(n1);
						break;
					}
				}
			}
			Console.WriteLine(result.Count());
			Console.WriteLine("No linq time=" + sw.ElapsedTicks);
			sw.Stop();
		}
		private static void DoNotUserLinqF(int[] array1, Flight[] f1)
		{
			List<int> result = new List<int>();
			var sw = Stopwatch.StartNew();
			foreach (int n1 in array1)
			{
				foreach (Flight f in f1)
				{
					if (n1 == f.CityNo)
					{
						result.Add(n1);
						break;
					}
				}
			}
			Console.WriteLine(result.Count());
			Console.WriteLine("No linq time Flight=" + sw.ElapsedTicks);
			sw.Stop();
		}
		/// <summary>
		/// linq
		/// </summary>
		/// <param name="array1"></param>
		/// <param name="array2"></param>
		private static void UserLinq(int[] array1, int[] array2)
		{
			List<int> result = new List<int>();
			var sw = Stopwatch.StartNew();
			//foreach (int n1 in array1)
			//{
			//	var exists = array2.Any(x => x.Equals(n1));
			//	if (exists)
			//	{
			//		result.Add(n1);
			//	}
			//}
			var intersect = array1.Intersect(array2).Distinct();
			Console.WriteLine(intersect.Count());
			Console.WriteLine(" linq time=" + sw.ElapsedTicks);
			
			sw.Stop();
		}
		private static void UserLinqF(int[] array1, Flight[] f1)
		{
			List<int> result = new List<int>();
			var sw = Stopwatch.StartNew();
			var intersect = from f in f1 where array1.Contains(f.CityNo) select f;
			Console.WriteLine(intersect.Count());
			Console.WriteLine(" linq time flight=" + sw.ElapsedTicks);

			sw.Stop();
		}
	}
}
