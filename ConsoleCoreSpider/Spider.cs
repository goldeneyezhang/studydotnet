using HtmlAgilityPack;
using HttpCode.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleCoreSpider
{
	public interface ISpider
	{
		void Crawl();
	}
	public class Spider : ISpider
	{
		public void Crawl()
		{
			int maxPageIndex = 10;//最大页数
			HttpHelpers httpHelpers = new HttpHelpers();
			for (int i = 1; i <=maxPageIndex; i++)
			{
				Console.WriteLine($"-----------------第{i}页------------------");
				HttpItems items = new HttpItems();
				System.Net.CookieContainer cc = new System.Net.CookieContainer();//自动处理Cookie对象
				items.Url = "https://www.cnblogs.com/mvc/AggSite/PostList.aspx";
				items.Method = "Post";//请求方式post
				items.Referer = "https://www.cnblogs.com/"; //referer头,如果需要请填写
				items.Container = cc;
				items.Postdata = "{\"CategoryType\":\"SiteHome\"," +
						"\"ParentCategoryId\":0," +
						"\"CategoryId\":808," +
						"\"PageIndex\":" + i + "," +
						"\"TotalPostCount\":4000," +
						"\"ItemListActionName\":\"PostList\"}";
				HttpResults hr = httpHelpers.GetHtml(items);
				//Console.WriteLine(hr.Html);
				//解析数据
				HtmlDocument doc = new HtmlDocument();
				//加载html
				doc.LoadHtml(hr.Html);
				//获取class=post_item_body的div列表
				HtmlNodeCollection itemNodes =
					doc.DocumentNode.SelectNodes("div[@class='post_item']/div[@class='post_item_body']");
				//循环根据每个div解析我们想要的数据
				foreach (var item in itemNodes)
				{
					//获取包含博文标题和地址的a标签
					var nodeA = item.SelectSingleNode("h3/a");
					//获取博文标题
					string title = nodeA.InnerText;
					//获取博文地址 a标签的 href属性
					string url = nodeA.GetAttributeValue("href", "");

					//获取包含作者名字的a标签
					var nodeAuthor = item.SelectSingleNode("div[@class='post_item_foot']/a[@class='lightblue']");
					string author = nodeAuthor.InnerText;
					Console.WriteLine($"标题：{title} | 作者：{author} | 地址：{url}");
				}
			}
			Console.ReadKey();
		}
	}
}
