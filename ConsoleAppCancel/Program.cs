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
    }
}
