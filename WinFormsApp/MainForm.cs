using ClassLibrary;
using ClassLibrary.Repo;
using System.Globalization;

namespace WinFormsApp
{
    public partial class MainForm : Form
    {
        public static readonly IRepo repo = RepoFactory.GetRepo();
        private bool comboBoxLoaded = false;

        public MainForm()
        {
            if (!Settings.SettingsExist(SettingsForm.fileName) &&
                new SettingsForm().ShowDialog() != DialogResult.OK)
                Application.Exit();
            ApplySettingsAndInitalize(true);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new SettingsForm().ShowDialog() == DialogResult.OK)
                ApplySettingsAndInitalize();
        }

        private void ApplySettingsAndInitalize(bool isStartup = false)
        {
            string[] settings = Settings.LoadSettings(SettingsForm.fileName);
            (var language, var gender) = (settings[0], settings[1]);
            Settings.GenderPath = gender == "Male" ? "men" : "women";
            CultureInfo culture = new(language == "English" ? "en" : "hr");
            Thread.CurrentThread.CurrentCulture = culture; // Globalizacija (vrijeme, datum, valuta)
            Thread.CurrentThread.CurrentUICulture = culture; // Lokalizacija (prijevodi)
            Controls.Clear();
            InitializeComponent();
            if (gender == "Female")
                Text = "FIFA Women's World Cup 2019";
            if (!isStartup)
                MainForm_Load(this, EventArgs.Empty);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var teams = repo.GetTeams().Select(t => $"{t.Country} ({t.FifaCode})");
            comboBox.Items.AddRange(teams.ToArray());
            var fileName = $"favorite-{Settings.GenderPath}-team.txt";
            comboBoxLoaded = false;
            if (Settings.SettingsExist(fileName))
                comboBox.Text = Settings.LoadSettings(fileName)[0];
            else button1.Enabled = false;
            comboBoxLoaded = true;
            LoadPlayers();
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxLoaded) return;
            Settings.SaveSettings($"favorite-{Settings.GenderPath}-team.txt", comboBox.Text);
            Settings.SaveSettings($"favorite-{Settings.GenderPath}-players.txt", "");
            favoritesPanel.Controls.Clear();
            button1.Enabled = true;
            LoadPlayers();
        }

        private void LoadPlayers()
        {
            var players = repo.GetPlayers(comboBox.Text.Split(" (")[0]);
            var playerControls = players.Select(p => new PlayerUserControl(p)).ToArray();
            playersPanel.Controls.Clear();
            playersPanel.Controls.AddRange(playerControls);
            var fileName = $"favorite-{Settings.GenderPath}-players.txt";
            if (Settings.SettingsExist(fileName))
                foreach (var playerName in Settings.LoadSettings(fileName))
                    playerControls.FirstOrDefault(
                        p => p.player.Name == playerName)?.setFavorite(true);
        }

        void panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        void panel_DragDrop(object sender, DragEventArgs e)
        {
            var draggedPlayer = (PlayerUserControl)e.Data!.GetData(typeof(PlayerUserControl));
            if (draggedPlayer.Parent == (Panel)sender)
                return;
            var playersToMove = draggedPlayer.Parent.Controls.Cast<PlayerUserControl>()
                .Where(player => player.BackColor == Color.LightGray || player == draggedPlayer)
                .ToList();
            var isFavoriting = (Panel)sender == favoritesPanel;
            if (isFavoriting && (favoritesPanel.Controls.Count + playersToMove.Count > 3))
            {
                MessageBox.Show("You can only favorite 3 players!");
                return;
            }
            foreach (var player in playersToMove)
                player.setFavorite(isFavoriting);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new RankingListsForm(comboBox.Text.Split(" (")[0]).Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {/* todo 
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = MessageBox.Show("Are you sure you want to exit?", "",
                    MessageBoxButtons.OKCancel) == DialogResult.Cancel;*/
        }
    }
}