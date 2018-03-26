using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.Model
{
	public class Topic
	{
		public int Id { get; set; }
		public string Text { get; set; }
	}
	public class TopicList
	{
		public List<Topic> list = new List<Topic>();
		public List<Topic> GetList()
		{
			if (list.Count == 0)
			{
				for (int i = 0; i < 10; i++)
				{
					list.Add(new Topic() { Id = i + 1, Text = "我是文章标题" + (i + 1) });
				}
			}
			return list;
		}
	}
}
