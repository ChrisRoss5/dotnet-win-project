using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp
{
    public partial class SettingsWindow : Window
    {
        public const string fileName = "settings.txt";

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            if (!Settings.SettingsExist(fileName))
                return;
            string[] settings = Settings.LoadSettings(fileName);
            languageComboBox.SelectedIndex = int.Parse(settings[0]);
            championshipComboBox.SelectedIndex = int.Parse(settings[1]);
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            var (languageIdx, championshipIdx) =
                (languageComboBox.SelectedIndex, championshipComboBox.SelectedIndex);
            if (languageIdx == -1 || championshipIdx == -1)
            {
                MessageBox.Show(FindResource("submitError") as string, 
                    FindResource("error") as string, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Settings.SaveSettings(fileName, languageIdx.ToString(), championshipIdx.ToString());
            DialogResult = true;
            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
