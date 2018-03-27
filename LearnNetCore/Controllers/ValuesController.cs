using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.Constraint;
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

    }
}