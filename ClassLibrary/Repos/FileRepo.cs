using ClassLibrary.Models;
using Newtonsoft.Json;

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
            var text = await File.ReadAllTextAsync(
                $"{Settings.solutionFolderPath}/worldcup.sfg.io/" +
                $"{Settings.ChampionshipPath}/{fileName}.json");
            return JsonConvert.DeserializeObject<T>(text, Converter.Settings)!;
        }
    }
}
