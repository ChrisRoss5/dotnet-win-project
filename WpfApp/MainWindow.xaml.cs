using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (/*!Settings.SettingsExist(SettingsWindow.fileName) && */new SettingsWindow().ShowDialog() == false)
                Environment.Exit(0);
            //ApplySettingsAndInitalize(true);
        }

        private void SetLanguageDictionary()
        {
            ResourceDictionary dict = new ResourceDictionary();
            string name = CultureInfo.CurrentUICulture.Name == "en" ? "" : ".hr";
            dict.Source = new Uri($"..\\Resources\\Resources{name}.xaml", UriKind.Relative);
            Resources.MergedDictionaries.Add(dict);
        }
    }
}
