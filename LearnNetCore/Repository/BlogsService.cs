using LearnNetCore.Context;
using LearnNetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Repository
{
	public interface IBlogService
	{
		void SaveBlog();
		List<Blog> GetBlogs();
	}
    public class BlogsService: IBlogService
    {
		public void SaveBlog()
		{
			using (var db = new BloggingContext())
			{
				var blog = new Blog { Url = "http://sample.com" };
				db.Blogs.Add(blog);
				db.SaveChanges();
			}
		}
		public List<Blog> GetBlogs()
		{
			using (var db = new BloggingContext())
			{
				var blogs = db.Blogs.ToList();
				return blogs;
			}
		}
    }
}
