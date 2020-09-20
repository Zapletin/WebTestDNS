using System.Text;
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(string command)
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
            Response.WriteAsync(answer, Encoding.GetEncoding(1251));
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
