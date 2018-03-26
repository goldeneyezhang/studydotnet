using Autofac;
using AutoMapper;
using EmitMapper;
using Serilog;
using Serilog.Core;
using System;
using System.Diagnostics;

namespace ConsoleAppMapper
{
    class Program
    {
        static void Main(string[] args)
        {
			var log = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().CreateLogger();
			Log.Logger = log;
			Console.WriteLine("Hello World!");
			//autofac 注入
			ContainerBuilder builder = new ContainerBuilder();
			builder.RegisterModule(new AutoMapperModule());
			builder.RegisterType<TinyConverter>().Keyed<IConvert>(ConvertType.TinyMapper);
			builder.RegisterType<EmitConverter>().Keyed<IConvert>(ConvertType.EmitMapper);
			builder.RegisterType<AutoConverter>().Keyed<IConvert>(ConvertType.AutoMapper);
			var container = builder.Build();
			//继承转换测试
			Inherit(container, 1000);
			Console.Read();
		}
		private static void Inherit(IContainer container,int times)
		{
			Test(container, ConvertType.AutoMapper, times);
			Test(container, ConvertType.EmitMapper, times);
			Test(container, ConvertType.TinyMapper, times);
		}
		private static void Test(IConvert convert, ConvertType ctype)
		{
			A2 obj = new A2 { Id = 1, Name = "A1" };
			A1 obj2 = convert.Convert<A2, A1>(obj);
			Show(obj, obj2, ctype);
		}
		private static void Test(IContainer container, ConvertType ctype, int times)
		{
			IConvert convert = default(IConvert);
			if (ctype == ConvertType.AutoMapper)
				convert = container.ResolveKeyed<IConvert>(ctype);
			else if (ctype == ConvertType.EmitMapper)
				convert = new EmitConverter();
			else if (ctype == ConvertType.TinyMapper)
				convert = new TinyConverter();
			if (times<1)
			{
				Test(convert, ctype);
				return;
			}
			Random r = new Random();
			A2 obj = new A2 { Id = r.Next(1, 1000), Name = "A1" };
			Stopwatch sw = new Stopwatch();
			sw.Start();
			for(int i=0;i<times;i++)
			{
				A1 obj2 = convert.Convert<A2, A1>(obj);
			}
			sw.Stop();
			Log.Information($"{ctype}执行了{times}次，耗时{sw.ElapsedMilliseconds}毫秒");
		}
		private static void Show(A2 a2,A1 a1, ConvertType ctype)
		{
			if(a1==null)
			{
				Log.Warning($"{ctype}转换失败");
			}
			else
			{
				Log.Debug($"a1==a2:{a2.Name==a1.Name}");
			}
		}
    }
}
