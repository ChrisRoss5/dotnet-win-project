using ClassLibrary.Models;
using ClassLibrary.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services
{
    public class WorldCupService : IWorldCupService
    {
        private readonly IRepo repo;

        public WorldCupService(IRepo repo)
        {
            this.repo = repo;
        }

        public Task<List<Team>> GetTeams() => repo.GetTeams();

        public Task<List<Match>> GetMatches(string countryCode) => repo.GetMatches(countryCode);

        public async Task<List<Player>> GetPlayers(string countryCode)
        {
            return (await GetMatches(countryCode))
                .SelectMany(m => m.HomeTeam.FifaCode == countryCode
                    ? m.HomeTeamStatistics.StartingEleven.Union(m.HomeTeamStatistics.Substitutes)
                    : m.AwayTeamStatistics.StartingEleven.Union(m.AwayTeamStatistics.Substitutes))
                .DistinctBy(p => p.ShirtNumber)
                .OrderByDescending(p => p.ShirtNumber)
                .ToList();
        }

        public async Task<List<KeyValuePair<Player, int>>> GetPlayersWithEventCount(string countryCode, TypeOfEvent _event)
        {
            var goalEvents = (await GetMatches(countryCode))
                .SelectMany(m => m.HomeTeam.FifaCode == countryCode ? m.HomeTeamEvents : m.AwayTeamEvents)
                .Where(e => e.TypeOfEvent == _event);
            return (await GetPlayers(countryCode))
                .Select(p => new KeyValuePair<Player, int>(p, goalEvents.Count(e => e.Player == p.Name)))
                .OrderByDescending(p => p.Value)
                .ToList();
        }
    }
}
