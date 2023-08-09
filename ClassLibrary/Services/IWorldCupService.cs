using ClassLibrary.Models;

namespace ClassLibrary.Services
{
    public interface IWorldCupService
    {
        Task<List<Team>> GetTeams();
        Task<List<Match>> GetMatches(string countryCode);
        Task<List<Player>> GetPlayers(string countryCode);
        Task<List<KeyValuePair<Player, int>>> GetPlayersWithEventCount(string countryCode, TypeOfEvent _event);
        Task<Result> GetResults(string countryCode);
    }
}
