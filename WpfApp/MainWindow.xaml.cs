using ClassLibrary;
using ClassLibrary.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ClassLibrary.Repo;
using System.ComponentModel;
using ClassLibrary.Models;
using System.Windows.Controls.Primitives;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        private readonly IWorldCupService worldCupService = new WorldCupService(new RestApiRepo());
        private readonly List<PlayerWindow> playerWindows = new();

        public MainWindow()
        {
            InitializeComponent();
            if (!Settings.SettingsExist(length: 3) && new SettingsWindow(true).ShowDialog() == false)
                Environment.Exit(0);
            ApplySettings(true);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (new SettingsWindow().ShowDialog() == true)
                ApplySettings();
        }

        private void ApplySettings(bool isStartup = false)
        {
            var settings = Settings.LoadSettings().Select(int.Parse).ToArray();
            (var language, var championship, var resolution) = (settings[0], settings[1], settings[2]);
            var prevChampionshipPath = Settings.ChampionshipPath;
            Settings.ChampionshipPath = championship == 0 ? "men" : "women";
            CultureInfo culture = new(language == 0 ? "en" : "hr");
            Thread.CurrentThread.CurrentCulture = culture;   // Globalizacija (vrijeme, datum, valuta)
            Thread.CurrentThread.CurrentUICulture = culture; // Lokalizacija (prijevodi)
            UpdateLanguageDictionary();
            var enTitle = $"{(championship == 0 ? "Men's" : "Women's")} World Cup";
            var hrTitle = $"{(championship == 0 ? "Muško" : "Žensko")} Svjetsko prvenstvo";
            Title = $"FIFA {(language == 0 ? enTitle : hrTitle)} {(championship == 0 ? "2018" : "2019")}";
            WindowState = resolution >= SettingsWindow.resolutions.Count
                ? WindowState.Maximized : WindowState.Normal;
            if (resolution < SettingsWindow.resolutions.Count)
                (Width, Height) = SettingsWindow.resolutions[resolution];
            if (!isStartup && prevChampionshipPath != Settings.ChampionshipPath)
                Window_Loaded(null, null);
        }

        private async void Window_Loaded(object? sender, RoutedEventArgs? e)
        {
            var teams = await worldCupService.GetTeams();
            firstTeamComboBox.ItemsSource = teams.Select(t => $"{t.Country} ({t.FifaCode})");
            secondTeamComboBox.ItemsSource = null;
            var fileName = $"favorite-{Settings.ChampionshipPath}-team.txt";
            if (Settings.SettingsExist(fileName))
                firstTeamComboBox.Text = Settings.LoadSettings(fileName)[0];
            else
            {
                firstTeamComboBox.SelectedIndex = -1;
                secondTeamComboBox.IsEnabled = false;
            }
        }

        private async void firstTeamComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearPlayers();
            resultLabel.Content = "vs";
            secondTeamComboBox.SelectedIndex = -1;
            if (firstTeamComboBox.SelectedIndex == -1) return;
            var countryCode = GetComboBoxCountryCode(firstTeamComboBox);
            var matches = await GetMatches(firstTeamComboBox);
            secondTeamComboBox.ItemsSource = matches
                .Select(m => m.HomeTeam.Code == countryCode ? m.AwayTeam : m.HomeTeam)
                .Select(t => $"{t.Country} ({t.Code})");
            secondTeamComboBox.IsEnabled = true;
        }

        private async void secondTeamComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (secondTeamComboBox.SelectedIndex == -1) return;
            var matches = await GetMatches(secondTeamComboBox);
            var match = matches.First(m =>
                m.HomeTeam.Code == GetComboBoxCountryCode(firstTeamComboBox) &&
                m.AwayTeam.Code == GetComboBoxCountryCode(secondTeamComboBox) ||
                m.HomeTeam.Code == GetComboBoxCountryCode(secondTeamComboBox) &&
                m.AwayTeam.Code == GetComboBoxCountryCode(firstTeamComboBox));
            var (firstTeam, secondTeam) = TeamsSwapped(match) ?
                (match.AwayTeam, match.HomeTeam) : (match.HomeTeam, match.AwayTeam);
            resultLabel.Content = $"{firstTeam.Goals} : {secondTeam.Goals}";
            LoadPlayers(match);
        }

        private void LoadPlayers(Match match)
        {
            ClearPlayers();
            foreach (var (teamStatistics, teamEvents, namePrefix) in new[] {
                (match.HomeTeamStatistics, match.HomeTeamEvents, TeamsSwapped(match) ? "second" : "first"),
                (match.AwayTeamStatistics, match.AwayTeamEvents, TeamsSwapped(match) ? "first" : "second")
            })
                foreach (var player in teamStatistics.StartingEleven)
                {
                    var playerUserControl = new PlayerUserControl(player);
                    var uniformGrid = (UniformGrid)FindName(namePrefix + "Team" + player.Position);
                    uniformGrid.Children.Add(playerUserControl);
                    var playerEvents = teamEvents.Where(e => e.Player == player.Name).ToList();
                    playerUserControl.MouseDown += (sender, e) =>
                    {
                        playerWindows.Add(new PlayerWindow(player, playerEvents));
                        playerWindows.Last().Show();
                    };
                }
        }

        private void ClearPlayers()
        {
            foreach (var playerWindow in playerWindows)
                playerWindow.Close();
            foreach (UniformGrid uniformGrid in ((UniformGrid)FindName("fieldGrid")).Children)
                uniformGrid.Children.Clear();
        }

        private bool TeamsSwapped(Match match)
            => match.HomeTeam.Code == GetComboBoxCountryCode(secondTeamComboBox);

        private static string GetComboBoxCountryCode(ComboBox comboBox)
            => comboBox.SelectedItem.ToString()!.Split('(', ')')[1];

        private Task<List<Match>> GetMatches(ComboBox comboBox)
            => worldCupService.GetMatches(GetComboBoxCountryCode(comboBox));

        private static void UpdateLanguageDictionary()
        {
            string name = CultureInfo.CurrentUICulture.Name == "en" ? "" : ".hr";
            Application.Current.Resources.MergedDictionaries.Add(new()
            {
                Source = new Uri($"..\\Resources\\Resources{name}.xaml", UriKind.Relative)
            });
        }

        private void firstTeamButton_Click(object sender, RoutedEventArgs e)
        {
            new TeamWindow(firstTeamComboBox.SelectedItem.ToString()!).Show();
        }

        private void secondTeamButton_Click(object sender, RoutedEventArgs e)
        {
            new TeamWindow(secondTeamComboBox.SelectedItem.ToString()!).Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (AppSettings.ConfirmDialogsEnabled)
                e.Cancel = MessageBox.Show(FindResource("exitConfirm") as string, FindResource("exit") as string,
                    MessageBoxButton.OKCancel, MessageBoxImage.Question) != MessageBoxResult.OK;
        }
    }
}
