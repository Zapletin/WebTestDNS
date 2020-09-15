using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebTestDNS.Models
{
    public class CommandContext : DbContext
    {
        public DbSet<CommandModel> Commands { get; set; }

        public CommandContext(DbContextOptions<CommandContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
