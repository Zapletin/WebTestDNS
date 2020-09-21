using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTestDNS.Models
{
    public class ResultModel
    {
        public List<CommandModel> Commands { get; set; }

        public string Result { get; set; }

        public ResultModel(List<CommandModel> commands, string result)
        {
            Commands = commands;
            Result = result != string.Empty ? $"Результат:\n{result}" : string.Empty;
        }
    }
}
