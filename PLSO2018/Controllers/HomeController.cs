﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PLSO2018.Website.Models;
using System.Diagnostics;

namespace PLSO2018.Website.Controllers {

	public class HomeController : Controller {

		public IConfiguration Configuration { get; set; }

		public HomeController(IConfiguration config) {
			Configuration = config;
		}


		[Authorize]
		public IActionResult Index() {
			return View();
		}

		public IActionResult About() {
			ViewData["Message"] = "Your application description page.";

			return View();
		}

		public IActionResult Contact() {
			ViewData["Message"] = "Your contact page.";

			return View();
		}

		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

	}
}
