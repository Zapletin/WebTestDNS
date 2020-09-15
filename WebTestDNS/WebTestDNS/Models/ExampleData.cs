using System;
using System.Collections.Generic;
using System.Linq;
using WebTestDNS.Models;

namespace WebTestDNS.Models
{
    public class ExampleData
    {
        public static void Initialize(CommandContext context)
        {
            if (!context.Commands.Any())
            {
                context.AddRange(new CommandModel("git status"), new CommandModel("git add ."), new CommandModel("ipconfig"));
                context.SaveChanges();
            }
        }
    }
}
