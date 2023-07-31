using ClassLibrary;
using ClassLibrary.Repo;
using System.Globalization;

namespace WinFormsApp
{
    public partial class MainForm : Form
    {
        public static readonly IRepo repo = RepoFactory.GetRepo();

        public MainForm()
        {
            if (!Settings.SettingsExist(SettingsForm.fileName))
            {
                SettingsForm settingsForm = new();
                settingsForm.ShowDialog();
            }
            string[] settings = Settings.LoadSettings(SettingsForm.fileName);
            (string language, string gender) = (settings[0], settings[1]);
            Settings.GenderPath = gender == "Male" ? "men" : "women";
            SetCultureAndInitalize(language);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string fileName = $"favorite-{Settings.GenderPath}-team";
            if (Settings.SettingsExist(fileName))
                comboBox.Text = Settings.LoadSettings(fileName)[0];
            comboBox.Items.AddRange(repo.GetTeams().ToArray());
            LoadPlayers();
        }

        void panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        void panel_DragDrop(object sender, DragEventArgs e)
        {
            foreach (PlayerUserControl playerControl in ((Panel)sender).Controls)
                if (playerControl.BackColor == Color.LightGray)
                    playerControl.setFavorite((Panel)sender == playersPanel);
            //((PlayerUserControl)e.Data!.GetData(typeof(PlayerUserControl))).Parent = (Panel)sender;
        }

        private void LoadPlayers()
        {
            var players = repo.GetPlayers(comboBox.Text);
            playersPanel.Controls.Clear();
            var playerControls = players.Select(p => new PlayerUserControl(p)).ToArray();
            playersPanel.Controls.AddRange(playerControls);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new();
            settingsForm.ShowDialog();
            string[] settings = Settings.LoadSettings(SettingsForm.fileName);
            SetCultureAndInitalize(settings[0]);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {/* todo 
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = MessageBox.Show("Are you sure you want to exit?", "",
                    MessageBoxButtons.OKCancel) == DialogResult.Cancel;*/
        }

        private void SetCultureAndInitalize(string lang)
        {
            CultureInfo culture = new(lang == "English" ? "en" : "hr");
            Thread.CurrentThread.CurrentCulture = culture; // Globalizacija (vrijeme, datum, valuta)
            Thread.CurrentThread.CurrentUICulture = culture; // Lokalizacija (prijevodi)
            this.Controls.Clear();
            InitializeComponent();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.SaveSettings($"favorite-{Settings.GenderPath}-team", comboBox.Text);
            LoadPlayers();
        }
    }
}