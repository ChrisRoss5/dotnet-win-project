using ClassLibrary.Repo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp
{
    public partial class RankingListsForm : Form
    {
        public static readonly IRepo repo = RepoFactory.GetRepo();
        private readonly string team;

        public RankingListsForm(string team)
        {
            InitializeComponent();
            Text = $"Ranking lists - {team}";
            this.team = team;
        }

        private void RankingListsForm_Load(object sender, EventArgs e)
        {
            var list1 = repo.GetPlayersWithGoals(team)
                .Select(pair => $"{pair.Key.Name} ({pair.Key.ShirtNumber}) - {pair.Value}")
                .ToList();
        }
    }
}
