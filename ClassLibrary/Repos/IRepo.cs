using ClassLibrary.Models;

namespace ClassLibrary.Repo
{
    public interface IRepo
    {
        List<Team> GetTeams();
        List<Player> GetPlayers(string country);
        List<KeyValuePair<Player, int>> GetPlayersWithEventCount(string country, TypeOfEvent _event);
        List<Match> GetMatches(string country);
    }
}
