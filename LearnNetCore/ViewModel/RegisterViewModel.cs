using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.ViewModel
{
    public class RegisterViewModel
    {
		[Required]
		[EmailAddress]
		[Display(Name="Email Address")]
		public string Email { get; set; }
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public AddressViewModel Address { get; set; }
		//public CountryEnum Country { get; set; } = CountryEnum.Canada;
		public string Country = "CA";
		public List<SelectListItem> Countries { get; } = new List<SelectListItem>
		{
			new SelectListItem { Value = "MX", Text = "Mexico" },
			new SelectListItem { Value = "CA", Text = "Canada" },
			new SelectListItem { Value = "US", Text = "USA"  },
		};
		public CountryEnum EnumCountry { get; set; } = CountryEnum.USA;
		public CountryViewModelGroup CountryGroup { get; set; } = new CountryViewModelGroup();
	}
}
