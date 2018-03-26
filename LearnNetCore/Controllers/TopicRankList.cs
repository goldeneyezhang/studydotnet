using LearnNetCore.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Controllers
{
    public class TopicRankList:ViewComponent
    {
		public IViewComponentResult Invoke()
		{
			TopicList topicList = new TopicList();
			var items = topicList.GetList();
			return View(items);
		}
    }
}
