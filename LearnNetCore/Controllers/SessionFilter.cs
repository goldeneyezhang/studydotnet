using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Controllers
{
	public class SessionLogAttribute : Attribute, IActionFilter
	{
		private readonly IRequestEntry _requestEntryMethod=new RequestEntryCollectionMethod();
		public void OnActionExecuted(ActionExecutedContext context)
		{
			var collection = _requestEntryMethod.GetOrCreateEntries(context.HttpContext);
			collection.RecordRequest(context.HttpContext.Request.PathBase + context.HttpContext.Request.Path);
			_requestEntryMethod.SaveEntries(context.HttpContext, collection);
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			return;
		}
	}
}
