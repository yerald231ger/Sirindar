﻿using System.Web.Mvc;

namespace Sirindar.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}