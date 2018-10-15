using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Entity;

namespace ConsoleApplicationWCFService
{
	class Program
	{
		static void Main(string[] args)
		{
			var builder = new ContainerBuilder();
			builder.RegisterType<TicketRestServiceClient>().As<ITicketRestService>();
			IContainer container = builder.Build();

			ITicketRestService ticketRestService = container.Resolve<ITicketRestService>();
			Console.WriteLine("GetAirlineInfos-----------------------");
			var resultAirline = ticketRestService.GetAirlineInfos();
			Console.WriteLine($"{resultAirline.Contents.Length},{resultAirline.ReturnCode}");
			Console.WriteLine("GetCityInfos-----------------------");
			var resultCity = ticketRestService.GetCityInfos();
			Console.WriteLine($"{resultCity.Contents.Length},{resultCity.ReturnCode}");
			Console.WriteLine("SearchFlightListForAggregate-----------------------");
			//var resultDate = ticketRestService.GetOutboundDate(DateTime.Now);
		
			//Console.WriteLine(result);
			Console.Read();
		}
	}
}
