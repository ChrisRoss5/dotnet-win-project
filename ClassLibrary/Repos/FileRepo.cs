using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
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
            return GetMatchesByCountry(country)
                .SelectMany(m => m.HomeTeam.Country == country
                    ? m.HomeTeamStatistics.StartingEleven.Union(m.HomeTeamStatistics.Substitutes)
                    : m.AwayTeamStatistics.StartingEleven.Union(m.AwayTeamStatistics.Substitutes))
                .DistinctBy(p => p.ShirtNumber)
                .OrderByDescending(p => p.ShirtNumber)
                .ToList();
        }

        public List<KeyValuePair<Player, int>> GetPlayersWithGoals(string country)
        {
            var goalEvents = GetMatchesByCountry(country)
                .SelectMany(m => m.HomeTeam.Country == country 
                    ? m.HomeTeamEvents : m.AwayTeamEvents)
                .Where(e => e.TypeOfEvent == TypeOfEvent.Goal);
            return GetPlayers(country).ToArray()
                .Select(p => new KeyValuePair<Player, int>(
                    p, goalEvents.Count(e => e.Player == p.Name)))
                .OrderByDescending(p => p.Value)
                .ToList();
        }

        private static IEnumerable<Match> GetMatchesByCountry(string country)
        {
            return ParseFile<List<Match>>("matches")!
                .Where(m => m.HomeTeam.Country == country || m.AwayTeam.Country == country);
        }

        private static T ParseFile<T>(string fileName)
        {
            var text = File.ReadAllText(
                $"{Settings.SolutionFolderPath}/worldcup.sfg.io/" +
                $"{Settings.GenderPath}/{fileName}.json");
            return JsonConvert.DeserializeObject<T>(text, Converter.Settings)!;
        }
    }
}
