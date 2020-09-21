using System.Text;
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
            var resultModel = new ResultModel(await context.Commands.ToListAsync(), string.Empty);
            return View(resultModel);
        }

        public async Task<IActionResult> PartialIndex(string answer)
        {
            var resultModel = new ResultModel(await context.Commands.ToListAsync(), answer);
            return PartialView("~/Views/Home/AddCommandForm.cshtml", resultModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string command)
        {
            context.Add(new CommandModel(command));
            context.SaveChanges();
            var cmd = new ProcessStartInfo("cmd.exe", $"/c {command}")
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            };
            var process = new Process() { StartInfo = cmd };
            string answer;
            if (process.Start())
            {
                answer = process.StandardOutput.ReadToEnd();
                process.Close();
            }
            else answer = "Не удалось запустить процесс";
            return await PartialIndex(answer);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
