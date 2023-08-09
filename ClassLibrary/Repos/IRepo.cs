using ClassLibrary.Models;

namespace ClassLibrary.Repo
{
    public interface IRepo
    {
        Task<List<Team>> GetTeams();
        Task<List<Match>> GetMatches(string countryCode);
        Task<List<Result>> GetResults();
    }
}
