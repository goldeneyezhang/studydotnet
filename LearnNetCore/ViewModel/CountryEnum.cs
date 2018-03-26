using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnNetCore.ViewModel
{
	public enum CountryEnum
	{
		[Display(Name = "United Mexican States")]
		Mexico,
		[Display(Name = "United  States of America")]
		USA,
		Canada,
		France,
		Germany,
		Spain
	}
}
