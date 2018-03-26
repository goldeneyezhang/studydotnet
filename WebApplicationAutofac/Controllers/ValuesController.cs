using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationAutofac.Controllers
{
	[Route("api/[controller]")]
	public class ValuesController : Controller
	{
		private readonly IGuidTransientAppService _guidTransientAppService;
		private readonly IGuidScopedAppService _guidScopedAppService;
		private readonly IGuidSingletonAppService _guidSingletonAppService;

		public ValuesController(IGuidTransientAppService guidTransientAppService, IGuidScopedAppService guidScopedAppService, IGuidSingletonAppService guidSingletonAppService)
		{
			_guidTransientAppService = guidTransientAppService;
			_guidScopedAppService = guidScopedAppService;
			_guidSingletonAppService = guidSingletonAppService;
		}
		// GET api/values
		[HttpGet]
		public IEnumerable<Guid> Get()
		{
			return new Guid[] { _guidTransientAppService.GuidItem(), _guidScopedAppService.GuidItem(), _guidSingletonAppService.GuidItem() };
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public string Get(int id)
		{
			//任务执行一次
			BackgroundJob.Enqueue(() => Console.WriteLine($"ASP.NET Core One Start LineZero{DateTime.Now},Id={id}"));
			//任务每分钟执行一次
			RecurringJob.AddOrUpdate(() => Console.WriteLine($"ASP.NET Core LineZero"), Cron.Minutely());
			//任务延时两分钟执行
			BackgroundJob.Schedule(() => Console.WriteLine($"ASP.NET Core await LineZero{DateTime.Now}"), TimeSpan.FromMinutes(2));
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody]string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
