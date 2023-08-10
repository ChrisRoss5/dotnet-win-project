using ClassLibrary.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace ClassLibrary.Repo
{
    public class FileRepo : IRepo
    {
        public Task<List<Team>> GetTeams()
        {
            return ParseFile<List<Team>>("teams");
        }

        public async Task<List<Match>> GetMatches(string countryCode)
        {
            return (await ParseFile<List<Match>>("matches"))
                .Where(m => m.HomeTeam.Code == countryCode || m.AwayTeam.Code == countryCode)
                .ToList();
        }

        public Task<List<Result>> GetResults()
        {
            return ParseFile<List<Result>>("results");
        }

        private static async Task<T> ParseFile<T>(string fileName)
        {
            string text;
            if (Environment.GetEnvironmentVariable("APP_ENV") == "Production")
                text = ReadResourceFile(fileName);
            else 
                text = await File.ReadAllTextAsync(
                    $"{Settings.solutionFolderPath}/worldcup.sfg.io/" +
                    $"{Settings.ChampionshipPath}/{fileName}.json");
            return JsonConvert.DeserializeObject<T>(text, Converter.Settings)!;
        }

        public static string ReadResourceFile(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceNames = assembly.GetManifestResourceNames();
            var resourceName = $"ClassLibrary.{Settings.ChampionshipPath}.{fileName}.json";
            using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
            using StreamReader reader = new(stream);
            string result = reader.ReadToEnd();
            return result;
        }
    }
}
