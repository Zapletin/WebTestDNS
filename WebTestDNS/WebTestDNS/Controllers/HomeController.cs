using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebTestDNS.Models;

namespace WebTestDNS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CommandContext context;

        public HomeController(ILogger<HomeController> logger, CommandContext context)
        {
            _logger = logger;
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(context.Commands);
        }

        public IActionResult PartialIndex()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string command)
        {
            try
            {
                context.Add(new CommandModel(command));
                context.SaveChanges();
                return RedirectToAction(nameof(PartialIndex));
            }
            catch
            {
                return PartialView();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
