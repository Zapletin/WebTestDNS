using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTestDNS.Models
{
    public class CommandModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CommandModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public CommandModel(string name) => Name = name;

        public override string ToString() => $"{Name}";
    }
}
