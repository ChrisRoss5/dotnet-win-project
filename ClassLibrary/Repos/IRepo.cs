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
        List<string> GetTeams();
        List<Player> GetPlayers(string team);
    }
}
