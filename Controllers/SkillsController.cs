﻿using Microsoft.AspNetCore.Mvc;

namespace LetsChatFinal.Controllers
{
    public class SkillsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
