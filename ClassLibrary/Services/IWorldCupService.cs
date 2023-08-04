using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Services
{
    public interface IWorldCupService
    {
        Task<List<Team>> GetTeams();
        Task<List<Match>> GetMatches(string countryCode);
        Task<List<Player>> GetPlayers(string countryCode);
        Task<List<KeyValuePair<Player, int>>> GetPlayersWithEventCount(string countryCode, TypeOfEvent _event);
    }
}
