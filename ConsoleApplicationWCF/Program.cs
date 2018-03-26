using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Entity.Rest;

namespace ConsoleApplicationWCF
{
	class Program
	{
		static void Main(string[] args)
		{
			TicketBasicServiceClient service = new TicketBasicServiceClient();
			Console.WriteLine("GetAirlineInfos-----------------------");
			var airLineInfos = service.GetAirlineInfos();
			foreach(AirlineInfo air in airLineInfos )
			{
				Console.WriteLine($"{air.ZhName},{air.EnName},{air.Code}");
			}
			Console.WriteLine("GetCacheInfo-----------------------");
			var cacheInfos = service.GetCacheInfo();
			foreach(CacheInfo cache in cacheInfos)
			{
				Console.WriteLine($"{cache.Key},{cache.ExpireTime},{cache.Description}");
			}
			Console.WriteLine("GetCityInfos-----------------------");
			var cityInfos = service.GetCityInfos();
			foreach(CityInfo city in cityInfos)
			{
				Console.WriteLine($"{city.Airports.FirstOrDefault().ZhName},{city.Country.EnName},{city.ZhName}");
			}
			Console.Read();
		}
	}
}
