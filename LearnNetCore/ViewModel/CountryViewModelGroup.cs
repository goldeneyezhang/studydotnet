using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.ViewModel
{
	public class CountryViewModelGroup
	{
		public string Country { get; set; }
		public IEnumerable<string> CountryCodes { get; } = new List<string>();
		public List<SelectListItem> Countries { get; set; }
		public CountryViewModelGroup()
		{
			var NorthAmericaGroup = new SelectListGroup { Name = "North America" };
			var EuropeGroup = new SelectListGroup { Name = "Europe" };
			Countries = new List<SelectListItem>
			{
				new SelectListItem
				{
					Value="MEX",
					Text="Mexico",
					Group=NorthAmericaGroup
				},
			new SelectListItem
			{
				Value = "CAN",
				Text = "Canada",
				Group = NorthAmericaGroup
			},
			new SelectListItem
			{
				Value = "US",
				Text = "USA",
				Group = NorthAmericaGroup
			},
			new SelectListItem
			{
				Value = "FR",
				Text = "France",
				Group = EuropeGroup
			},
			new SelectListItem
			{
				Value = "ES",
				Text = "Spain",
				Group = EuropeGroup
			},
			new SelectListItem
			{
				Value = "DE",
				Text = "Germany",
				Group = EuropeGroup

			}
		};
		}
	}
}
