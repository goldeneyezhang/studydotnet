using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.Constraint;
using LearnNetCore.Model;
using LearnNetCore.SelfAttribute;
using Microsoft.AspNetCore.Mvc;

namespace LearnNetCore.Controllers
{
	[FormatFilter]
    public class ValuesController : Controller
    {
		[AddHeader("Author", "Steve Smith @ardalis")]
		[HttpGet("[controller]/[action].{format}")]
        public  IEnumerable<string> Get()
        {
			return new string[] { "value1", "value2" };
        }
		private IEnumerable<User> Users;
		public ValuesController()
		{
			Users = new User[] {
				new User(){Id=1,UserName="book",Age=1 },
				new User(){Id=2,UserName="asp.net core",Age=10 }
			};
		}
		[HttpGet]
		[Produces("application/proto")]
		public IEnumerable<User> GetUsers()
		{
			return Users;
		}
		[HttpGet("{id}")]
		[Produces("application/proto")]
		public User Get(int id)
		{
			return Users.FirstOrDefault(r => r.Id == id);
		}
    }
}