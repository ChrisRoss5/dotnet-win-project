using ClassLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp
{
    public partial class SettingsWindow : Window
    {
        private readonly bool isFirstStartup = false;
        private List<ComboBox> comboBoxes = null!;
        public static readonly List<(int w, int h)> resolutions = new()
        {
            ( 800, 600 ),
            ( 1024, 768 ),
            ( 1280, 800 )
        };

        public SettingsWindow(bool isFirstStartup = false)
        {
            InitializeComponent();
            this.isFirstStartup = isFirstStartup;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            resolutionComboBox.ItemsSource = resolutions.Select(r => $"{r.w}x{r.h}")
                .Concat(new[] { FindResource("fullscreen") as string });
            comboBoxes = new List<ComboBox> { languageComboBox, championshipComboBox, resolutionComboBox };
            if (!Settings.SettingsExist()) return;
            var settings = Settings.LoadSettings();
            for (int i = 0; i < settings.Length; i++)
                comboBoxes[i].SelectedIndex = int.Parse(settings[i]);
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            var indexes = comboBoxes.Select(el => el.SelectedIndex.ToString());
            if (indexes.Any(el => el == "-1"))
            {
                MessageBox.Show(FindResource("submitError") as string,
                    FindResource("error") as string, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!isFirstStartup && AppSettings.ConfirmDialogsEnabled &&
                MessageBox.Show(FindResource("confirmSettings") as string,
                FindResource("confirmSettingsTitle") as string,
                MessageBoxButton.OKCancel, MessageBoxImage.Question) != MessageBoxResult.OK)
                return;
            Settings.SaveSettings(settings: indexes.ToArray());
            DialogResult = true;
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
