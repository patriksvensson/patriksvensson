using Generator.Commands;
using Microsoft.Toolkit.Parsers.Rss;
using Spectre.Console.Cli;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Generator
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var app = new CommandApp<GenerateCommand>();
            return await app.RunAsync(args);
        }
    }
}
