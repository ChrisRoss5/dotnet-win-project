using ClassLibrary.Models;
using Newtonsoft.Json;

namespace ClassLibrary.Repo
{
    public class FileRepo : IRepo
    {
        public List<Team> GetTeams()
        {
            return ParseFile<List<Team>>("teams");
        }

        public List<Player> GetPlayers(string country)
        {
            return GetMatches(country)
                .SelectMany(m => m.HomeTeam.Country == country
                    ? m.HomeTeamStatistics.StartingEleven.Union(m.HomeTeamStatistics.Substitutes)
                    : m.AwayTeamStatistics.StartingEleven.Union(m.AwayTeamStatistics.Substitutes))
                .DistinctBy(p => p.ShirtNumber)
                .OrderByDescending(p => p.ShirtNumber)
                .ToList();
        }

        public List<KeyValuePair<Player, int>> GetPlayersWithEventCount(string country, TypeOfEvent _event)
        {
            var goalEvents = GetMatches(country)
                .SelectMany(m => m.HomeTeam.Country == country ? m.HomeTeamEvents : m.AwayTeamEvents)
                .Where(e => e.TypeOfEvent == _event);
            return GetPlayers(country).ToArray()
                .Select(p => new KeyValuePair<Player, int>(p, goalEvents.Count(e => e.Player == p.Name)))
                .OrderByDescending(p => p.Value)
                .ToList();
        }

        public List<Match> GetMatches(string country)
        {
            return ParseFile<List<Match>>("matches")!
                .Where(m => m.HomeTeam.Country == country || m.AwayTeam.Country == country)
                .ToList();
        }

        private static T ParseFile<T>(string fileName)
        {
            var text = File.ReadAllText(
                $"{Settings.SolutionFolderPath}/worldcup.sfg.io/" +
                $"{Settings.ChampionshipPath}/{fileName}.json");
            return JsonConvert.DeserializeObject<T>(text, Converter.Settings)!;
        }
    }
}
