using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.Model;
using LearnNetCore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LearnNetCore.Controllers
{
    public class BlogsController : Controller
    {
		private readonly IBlogService _blogService;
		public BlogsController(IBlogService blogService)
		{
			this._blogService = blogService;
		}
		public IActionResult Index()
        {
			//_blogService.SaveBlog();
			var result = _blogService.GetBlogs();
			return View(result);
        }
		public IActionResult AuthorPartial(string author)
		{
			return View(author);
		}
		public IActionResult ArticleSection(Blog blog)
		{
			return View(blog);
		}
    }
}