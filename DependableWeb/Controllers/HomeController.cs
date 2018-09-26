﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DependableWeb.Models;
using DependableWeb.Interfaces;

namespace DependableWeb.Controllers
{
    public class HomeController : Controller
    {
		private readonly IDateTime _dateTime;

		public HomeController(IDateTime dateTime)
		{
			 _dateTime = dateTime;
			
		}

		public IActionResult Index()
        {
			var serverTime = _dateTime.Now;
			if (serverTime.Hour < 12)
			{
				ViewData["Message"] = "It's morning here - Good Morning!";
			}
			else if (serverTime.Hour < 17)
			{
				ViewData["Message"] = "It's afternoon here - Good Afternoon!";
			}
			else
			{
				ViewData["Message"] = "It's evening here - Good Evening!";
			}
			
			return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
