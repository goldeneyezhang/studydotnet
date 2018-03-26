using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LearnNetCore.Controllers
{
    public class UrlGeneController : Controller
    {
		[HttpGet("source")]
        public IActionResult Source()
        {
			var url = Url.Action("Destination");
			return Content($"Go check out {url},it's really great.");
        }
		[HttpGet("custom/url/to/destination")]
		public IActionResult Destination()
		{
			return View();
		}
    }
}