using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.Constraint;
using Microsoft.AspNetCore.Mvc;

namespace LearnNetCore.Controllers
{
    public class ValuesController : Controller
    {
		[HttpGet]
        public  IEnumerable<string> Get()
        {
			return new string[] { "value1", "value2" };
        }
    }
}