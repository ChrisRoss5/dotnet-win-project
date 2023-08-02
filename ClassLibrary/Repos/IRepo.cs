using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repo
{
    public interface IRepo
    {
        List<Team> GetTeams();
        List<Player> GetPlayers(string country);
        List<KeyValuePair<Player, int>> GetPlayersWithGoals(string country);
    }
}
