using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            return View(await context.Commands.ToListAsync());
        }

        public async Task<IActionResult> PartialIndex()
        {
            return PartialView("~/Views/Home/AddCommandForm.cshtml", await context.Commands.ToListAsync());
        }

        [HttpPost]
        public IActionResult Create(string command)
        {
            context.Add(new CommandModel(command));
            context.SaveChanges();
            return RedirectToAction(nameof(PartialIndex));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
