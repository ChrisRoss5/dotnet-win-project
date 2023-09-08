using ClassLibrary.Repo;
using ClassLibrary.Services;
using System.Windows;

namespace WpfApp
{
    public partial class TeamWindow : Window
    {
        private readonly IWorldCupService worldCupService;
        private readonly string countryCode;

        public TeamWindow(IWorldCupService worldCupService, string team)
        {
            this.worldCupService = worldCupService;
            InitializeComponent();
            Title = $"{FindResource("results") as string} - {team}";
            countryCode = team.Split('(', ')')[1];
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var results = await worldCupService.GetResults(countryCode);
            teamNameLabel.Content = results.Country;
            fifaCodeLabel.Content = results.FifaCode;
            gamesLabel.Content = results.GamesPlayed.ToString();
            winsLabel.Content = results.Wins.ToString();
            lossesLabel.Content = results.Losses.ToString();
            drawsLabel.Content = results.Draws.ToString();
            goalsScoredLabel.Content = results.GoalsFor.ToString();
            goalsConceivedLabel.Content = results.GoalsAgainst.ToString();
            goalDifferenceLabel.Content = results.GoalDifferential.ToString();
        }
    }
}
