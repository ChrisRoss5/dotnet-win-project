using ClassLibrary;
using ClassLibrary.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfApp
{
    public partial class PlayerUserControl : UserControl
    {
        private readonly Player player;

        public PlayerUserControl(Player player)
        {
            InitializeComponent();
            this.player = player;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var path = UserSettings.GetPlayerImagePath(player.Name);
            if (path != "")
                image.Source = new BitmapImage(new Uri(path));
            textBlock.Text = $"{player.Name} ({player.ShirtNumber})";
        }
    }
}
