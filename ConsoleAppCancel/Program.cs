using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppCancel
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(RemoveDuplicates(new int[] { 1, 1, 2, 2, 3, 3 }));
			JSchemaGenerator generator = new JSchemaGenerator();
			generator.DefaultRequired = Required.DisallowNull;

			JSchema schema = generator.Generate(typeof(PostalAddress));
			Console.WriteLine(schema.ToString());
			var cancelTokenSource = new CancellationTokenSource();

			Task.Factory.StartNew(() =>
			{
				while (!cancelTokenSource.IsCancellationRequested)
				{
					Console.WriteLine(DateTime.Now);
					Thread.Sleep(1000);
				}
			}, cancelTokenSource.Token);

			Console.WriteLine("Press any key to cancel");
			Console.ReadLine();
			cancelTokenSource.Cancel();
			Console.WriteLine("Done");
			Console.ReadLine();
			Console.WriteLine("Hello World!");


		}
		public static int RemoveDuplicates(int[] nums)
		{
			if (nums.Length == 0)
			{
				return 0;
			}
			int number = 0;
			for (int i = 0; i < nums.Length; i++)
			{
				if (nums[i] != nums[number])
				{
					number++;
					nums[number] = nums[i];
				}
			}
			return ++number;
		}
	}
}
