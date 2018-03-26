using LearnNetCore.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Controllers
{
	[Area("Blog")]
    public class UserController:Controller
    {
		private DataContext _context;
		public UserController(DataContext context)
		{
			this._context = context;
		}
		public IActionResult Index()
		{
			var result = _context.Users.ToList();
			var url = Url.Action("Index");
			return View(result);
		}
    }
}
