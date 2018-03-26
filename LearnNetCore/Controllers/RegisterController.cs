using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LearnNetCore.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
			RegisterViewModel registerViewModel = new RegisterViewModel();
			registerViewModel.Country = CountryEnum.Canada;

			return View(registerViewModel);
        }
    }
}