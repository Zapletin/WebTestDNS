using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTestDNS.Models;

namespace WebTestDNS.Controllers
{
    public class ResultController : Controller
    {
        private CommandContext context;

        public ResultController(CommandContext context) => this.context = context;

        public async Task<ActionResult> Index()
        {
            return View(await context.Commands.ToListAsync());
        }

    }
}