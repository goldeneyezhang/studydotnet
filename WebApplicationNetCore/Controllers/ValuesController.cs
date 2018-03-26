using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationNetCore.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
		private static int count = 0;
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
			count = 0;
			return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public bool Post([FromBody]dynamic value)
        {
			Thread.Sleep(3000);
			Interlocked.Add(ref count, 1);
			Console.WriteLine($"received={count},{DateTime.Now.ToString()}");
			return true;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
