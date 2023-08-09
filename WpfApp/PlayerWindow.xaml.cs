using ClassLibrary;
using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApp
{
    public partial class PlayerWindow : Window
    {
        private readonly Player player;
        private readonly List<TeamEvent> playerEvents;

        public PlayerWindow(Player player, List<TeamEvent> playerEvents)
        {
            InitializeComponent();
            this.player = player;
            this.playerEvents = playerEvents;
            Title = $"{player.Name} ({player.ShirtNumber})";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var path = Settings.GetPlayerImagePath(player.Name);
            if (path != "")
                image.Source = new BitmapImage(new Uri(path));
            nameLabel.Content = $"{player.Name} ({player.ShirtNumber})";
            captainLabel.Visibility = player.Captain ? Visibility.Visible : Visibility.Collapsed;
            goalsScoredLabel.Content = playerEvents.Where(e => e.TypeOfEvent == TypeOfEvent.Goal).Count();
            yellowCardsLabel.Content = playerEvents.Where(e => 
                e.TypeOfEvent == TypeOfEvent.YellowCard || 
                e.TypeOfEvent == TypeOfEvent.YellowCardSecond).Count();
        }
    }
}
