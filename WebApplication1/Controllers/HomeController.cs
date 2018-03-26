using Autofac;
using ConsoleAppAutofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		private ILog _log;
		private IProduct _product;
		public HomeController()
		{

		}
		public HomeController(ILog log,IProduct product)
		{
			this._log = log;
			this._product = product;
		}
		public ActionResult Index()
		{
			_log.SaveLog("Index");
			_product.Run();
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}