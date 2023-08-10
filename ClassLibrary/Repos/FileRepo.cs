using ClassLibrary.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace ClassLibrary.Repo
{
    public class FileRepo : IRepo
    {
        public Task<List<Team>> GetTeams()
            => ParseFile<List<Team>>("teams");

        public async Task<List<Match>> GetMatches(string countryCode)
        {
            return (await ParseFile<List<Match>>("matches"))
                .Where(m => m.HomeTeam.Code == countryCode || m.AwayTeam.Code == countryCode)
                .ToList();
        }

        public Task<List<Result>> GetResults()
            => ParseFile<List<Result>>("results");

        private static async Task<T> ParseFile<T>(string fileName)
        {
            var text = await (AppSettings.IsProduction
                ? ReadResourceFile(fileName)
                : File.ReadAllTextAsync(
                    $"{AppSettings.SolutionPath}/worldcup.sfg.io/" +
                    $"{UserSettings.ChampionshipPath}/{fileName}.json"));
            return JsonConvert.DeserializeObject<T>(text, Converter.Settings)!;
        }

        private static Task<string> ReadResourceFile(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"ClassLibrary.{UserSettings.ChampionshipPath}.{fileName}.json";
            using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
            using StreamReader reader = new(stream);
            return reader.ReadToEndAsync();
        }
    }
}
