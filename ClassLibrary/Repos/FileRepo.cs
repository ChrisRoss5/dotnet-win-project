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
        public List<string> GetTeams()
        {
            var teams = ParseFile<List<Team>>("teams");
            return teams!.Select(t => $"{t.Country} ({t.FifaCode})").ToList();
        }
        public List<Player> GetPlayers(string team)
        {
            var matches = ParseFile<List<Match>>("matches");
            var players = matches!
                .Where(m => m.HomeTeam.Country == team.Split(" (")[0])
                .SelectMany(m => m.HomeTeamStatistics.StartingEleven.Union(m.HomeTeamStatistics.Substitutes))
                .DistinctBy(p => p.ShirtNumber).OrderByDescending(p => p.ShirtNumber).ToList();
            return players;
        }

        private static T ParseFile<T>(string fileName)
        {
            var path = $"{Settings.SolutionFolderPath}/worldcup.sfg.io/" +
                $"{Settings.GenderPath}/{fileName}.json";
            var json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json, Converter.Settings)!;
        }
    }
}
