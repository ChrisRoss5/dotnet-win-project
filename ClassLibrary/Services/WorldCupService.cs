using ClassLibrary.Models;
using ClassLibrary.Repo;

namespace ClassLibrary.Services
{
    public class WorldCupService : IWorldCupService
    {
        private readonly IRepo repo = AppSettings.DefaultRepo;

        public WorldCupService(IRepo? repo = null)
        {
            if (repo != null && !AppSettings.ForceDefaultRepo)
                this.repo = repo;
        }

        public Task<List<Team>> GetTeams() => repo.GetTeams();

        public Task<List<Match>> GetMatches(string countryCode) => repo.GetMatches(countryCode);

        public async Task<List<Player>> GetPlayers(string countryCode)
        {
            return (await GetMatches(countryCode))
                .SelectMany(m => m.HomeTeam.Code == countryCode
                    ? m.HomeTeamStatistics.StartingEleven.Union(m.HomeTeamStatistics.Substitutes)
                    : m.AwayTeamStatistics.StartingEleven.Union(m.AwayTeamStatistics.Substitutes))
                .DistinctBy(p => p.ShirtNumber)
                .OrderByDescending(p => p.ShirtNumber)
                .ToList();
        }

        public async Task<List<KeyValuePair<Player, int>>> GetPlayersWithEventCount(string countryCode, TypeOfEvent _event)
        {
            var goalEvents = (await GetMatches(countryCode))
                .SelectMany(m => m.HomeTeam.Code == countryCode ? m.HomeTeamEvents : m.AwayTeamEvents)
                .Where(e => e.TypeOfEvent == _event);
            return (await GetPlayers(countryCode))
                .Select(p => new KeyValuePair<Player, int>(p, goalEvents.Count(e => e.Player == p.Name)))
                .OrderByDescending(p => p.Value)
                .ToList();
        }

        public async Task<Result> GetResults(string countryCode)
        {
            return (await repo.GetResults()).Find(r => r.FifaCode == countryCode)!;
        }
    }
}
