using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Serilog;
using Serilog.Context;
using Serilog.Sinks.MSSqlServer;

namespace ConsoleAppString
{
	class Program
	{
		static void Main(string[] args)
		{
			string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
			Console.WriteLine(hostName);
			Console.WriteLine(Environment.MachineName);
			// Get the IP  
			var myIPs = Dns.GetHostEntry(hostName).AddressList.Where(x=> !x.IsIPv6LinkLocal).ToList();
			foreach (IPAddress ip in myIPs)
			{
					Console.WriteLine("My IP Address is :" + ip.ToString());
			}
			//IP();
			//TestWebClient();
			var log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
			log.Information("Hello, Serilog!");
			log.Debug("Hello Debug");
			log.Warning("Hello Warn");
			log.Error("Hello Error");
			var log2 = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console()
				.WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day)
				.CreateLogger();
			log2.Information("Hello,world!");
			int a = 10, b = 0;
			try
			{
				log2.Debug("Dividing {A} by {B}", a, b);
				Console.WriteLine(a / b);
			}
			catch (Exception ex)
			{
				log2.Error(ex, "Something went wrong");
			}
			Log.CloseAndFlush();
			Log.Logger = new LoggerConfiguration()
			.Enrich.With(new ThreadIdEnricher())
			.WriteTo.Console(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
			.CreateLogger();
			Log.Information("Enrich Test");
			var sensorInput = new { Latitude = 25, Longitude = 134 };
			Log.Information("Processing {@SensorInput}", sensorInput);
			var unknown = new[] { 1, 2, 3 };
			Log.Information("Received {Data}", unknown);
			var myLog = Log.ForContext<Program>();
			myLog.Information("Hello!");
			var connectionString = @"Data Source=DST58268\SQLEXPRESS;Initial Catalog=WebApplicationAuth2;Integrated Security=True;";  // or the name of a connection string in your .config file
			var tableName = "Logs";
			var columnOptions = new ColumnOptions();  // optional

			var sqllog = new LoggerConfiguration()
				.WriteTo.MSSqlServer(connectionString, tableName, columnOptions: columnOptions)
				.CreateLogger();
			sqllog.Information("my first sql log");
			var enrichThreadIdLog = new LoggerConfiguration().Enrich.WithThreadId().WriteTo.Console().CreateLogger();
			enrichThreadIdLog.Warning("Thread Id Log");
			var logContext = new LoggerConfiguration().Enrich.FromLogContext().WriteTo.Console().CreateLogger();
			logContext.Information("No contextual properties");
			using (LogContext.PushProperty("A", 1))
			{
				logContext.Information("Carries property A=1");
				using (LogContext.PushProperty("A", 2))
				using (LogContext.PushProperty("B", 2))
				{
					logContext.Information("Carries A=2 and B=1");
				}
				logContext.Information("Carries property A=1,again ");
			}
			var logFormat = new LoggerConfiguration().WriteTo.LiterateConsole().CreateLogger();
			var exampleUser = new User { Id = 1, Name = "Adam", Created = DateTime.Now };
			logFormat.Information("Created{@user} on {Created}", exampleUser, DateTime.Now);
			Console.Read();
		}
		private static void IP()
		{
			//网卡信息类
			NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface adap in adapters)
			{
				Console.WriteLine("CardName::" + adap.Name + " /Speed::" + adap.Speed + " /MAC::" + BitConverter.ToString(adap.GetPhysicalAddress().GetAddressBytes()));
				IPInterfaceProperties ipProperties = adap.GetIPProperties();

				GatewayIPAddressInformationCollection gateways = ipProperties.GatewayAddresses;
				foreach (var tmp in gateways)
				{
					Console.WriteLine("Gateway>>>" + tmp.Address);
				}
				IPAddressCollection dnsAddress = ipProperties.DnsAddresses;
				foreach (IPAddress tmp in dnsAddress)
				{
					Console.WriteLine("DNS>>>" + BitConverter.ToString(tmp.GetAddressBytes()));
				}
			}
		}
		private void TestToLower()
		{
			string[] words = { "Tuesday", "Salı", "Вторник", "Mardi",
						 "Τρίτη", "Martes", "יום שלישי",
						 "الثلاثاء", "วันอังคาร" };
			Console.BufferHeight = 1000;
			var test = CultureInfo.GetCultures(CultureTypes.AllCultures)
								  .Select(ci =>
								  {
									  string[] wordsToLower = words.Select(x => x.ToLower(ci)).ToArray();
									  string[] wordsToLowerInvariant = words.Select(x => x.ToLowerInvariant()).ToArray();
									  return new
									  {
										  Culture = ci,
										  ToLowerDiffers = !wordsToLower.SequenceEqual(wordsToLowerInvariant)
									  };
								  })
								  .ToArray();
			foreach (var x in test)
			{
				Console.WriteLine("Culture {0}, ToLower and ToLowerInvariant produces different results: {1}", x.Culture, x.ToLowerDiffers);
			}
			Console.WriteLine();
			Console.WriteLine("Difference exists for any ToLower call: {0}", test.Any(x => x.ToLowerDiffers));
		}
		private static async Task TestWebClient()
		{
			Console.WriteLine("Starting connections");
			for (int i = 0; i < 10; i++)
			{
				using (var client = new HttpClient())
				{
					var result = await client.GetAsync("http://aspnetmonsters.com");
					Console.WriteLine(result.StatusCode);
				}
			}
			Console.WriteLine("Connection done");
		}
	}
}
