using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LearnNetCore.Controllers
{
	[RabbitController]
	public class HomeController : Controller
	{
		private readonly IOptions<MyOptions> _options;
		private readonly ILogger<HomeController> _logger;
		public HomeController(IOptions<MyOptions> options,ILogger<HomeController> logger)
		{
			_options = options;
			_logger = logger;
		}
		[HttpGet]
		
		public MyOptions Get()
		{
			_logger.LogInformation("HomeGet", "Get Options");
			_logger.LogWarning("I am warning","hehe");
			return _options.Value;
		}
	}
}
