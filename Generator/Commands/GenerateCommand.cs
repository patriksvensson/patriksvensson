using Microsoft.Toolkit.Parsers.Rss;
using Scriban;
using Spectre.Cli;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Generator.Commands
{
    public sealed class GenerateCommand : AsyncCommand<GenerateCommand.Settings>
    {
        public sealed class Settings : CommandSettings
        {
            [CommandArgument(0, "<TEMPLATE>")]
            public string Template { get; set; }

            [CommandArgument(1, "<OUTPUT>")]
            public string Output { get; set; }
        }

        public override ValidationResult Validate(CommandContext context, Settings settings)
        {
            if (!File.Exists(settings.Template))
            {
                return ValidationResult.Error("Template file does not exist.");
            }
            return ValidationResult.Success();
        }

        public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
        {
            // Parse the RSS
            using var client = new HttpClient();
            var xml = await client.GetStringAsync("https://patriksvensson.se/rss.xml");
            var parser = new RssParser();
            var rss = parser.Parse(xml).Take(5).ToList();

            // Parse template
            var template = Template.Parse(File.ReadAllText(settings.Template));
            var result = template.Render(new { rss });

            // Write the result back
            File.WriteAllText(settings.Output, result);

            return 0;
        }
    }
}
