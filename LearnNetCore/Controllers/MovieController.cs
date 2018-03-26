using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LearnNetCore.Controllers
{
    public class MovieController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
			Movie movie = new Movie()
			{
				ID = 1,
				Title = "Movie",
				ReleaseDate=DateTime.Now,
				Genre="Pop",
				Price=100
			};
            return View(movie);
        }
    }
}
