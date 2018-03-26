using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LearnNetCore.Controllers
{
	public class SpeakerController : Controller
	{
		private List<Speaker> Speakers = new List<Speaker>()
		{
			new Speaker{SpeakerId=10},
			new Speaker{SpeakerId=11},
			new Speaker{SpeakerId=12}
		};
		// GET: /<controller>/
		public IActionResult Index()
		{
			return View(Speakers);
		}
		[Route("Speaker/{id:int}")]
		public IActionResult Detail(int id) =>
			View(Speakers.FirstOrDefault(a => a.SpeakerId == id));

		[Route("/Speaker/Evaluations", Name = "speakerevals")]
		public IActionResult Evaluations() => View();

		[Route("/Speaker/EvaluationsCurrent", Name = "speakerevalscurrent")]
		public IActionResult Evaluations(int speakerId, bool currentYear) => View();

		public IActionResult AnchorTagHelper(int id)
		{
			var speaker = new Speaker
			{
				SpeakerId = id
			};
			return View(speaker);
		}
	}
}
