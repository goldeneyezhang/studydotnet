using Autofac;
using System;

namespace ConsoleCoreSpider
{
    class Program
    {
        static void Main(string[] args)
        {
			ContainerBuilder builder = new ContainerBuilder();
			builder.RegisterType<Spider>().As<ISpider>();
			var container = builder.Build();
			ISpider spider = container.Resolve<ISpider>();
			spider.Crawl();
			
			Console.Read();
        }
    }
}
