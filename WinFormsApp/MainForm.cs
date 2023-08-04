using ClassLibrary;
using ClassLibrary.Repo;
using ClassLibrary.Services;
using System.Globalization;
using System.Resources;
using WinFormsApp.Properties;

namespace WinFormsApp
{
    public partial class MainForm : Form
    {
        private readonly IWorldCupService worldCupService = new WorldCupService(new RestApiRepo());
        private readonly ResourceManager rm = new(typeof(Resources));
        private bool comboBoxLoaded = false;

        public MainForm()
        {
            if (!Settings.SettingsExist(SettingsForm.fileName)
                && new SettingsForm().ShowDialog() != DialogResult.OK)
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
            (var language, var championship) = (settings[0], settings[1]);
            Settings.ChampionshipPath = championship == "0" ? "men" : "women";
            CultureInfo culture = new(language == "0" ? "en" : "hr");
            Thread.CurrentThread.CurrentCulture = culture;   // Globalizacija (vrijeme, datum, valuta)
            Thread.CurrentThread.CurrentUICulture = culture; // Lokalizacija (prijevodi)
            Controls.Clear();
            InitializeComponent();
            var enTitle = $"{(championship == "0" ? "Men's" : "Women's")} World Cup";
            var hrTitle = $"{(championship == "0" ? "Muško" : "Žensko")} Svjetsko prvenstvo";
            Text = $"FIFA {(language == "0" ? enTitle : hrTitle)} 2019";
            if (!isStartup)
                MainForm_Load(this, EventArgs.Empty);
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            var teams = await worldCupService.GetTeams();
            comboBox.Items.AddRange(teams.Select(t => $"{t.Country} ({t.FifaCode})").ToArray());
            var fileName = $"favorite-{Settings.ChampionshipPath}-team.txt";
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
            Settings.SaveSettings($"favorite-{Settings.ChampionshipPath}-team.txt", comboBox.Text);
            Settings.SaveSettings($"favorite-{Settings.ChampionshipPath}-players.txt", "");
            favoritesPanel.Controls.Clear();
            button1.Enabled = true;
            LoadPlayers();
        }

        private async void LoadPlayers()
        {
            var countyCode = comboBox.Text.Split('(', ')')[1];
            var players = await worldCupService.GetPlayers(countyCode);
            var playerControls = players.Select(p => new PlayerUserControl(p)).ToArray();
            playersPanel.Controls.Clear();
            playersPanel.Controls.AddRange(playerControls);
            var fileName = $"favorite-{Settings.ChampionshipPath}-players.txt";
            if (Settings.SettingsExist(fileName))
                foreach (var playerName in Settings.LoadSettings(fileName))
                    playerControls.FirstOrDefault(
                        p => p.player.Name == playerName)?.setFavorite(true);
        }

        private void panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void panel_DragDrop(object sender, DragEventArgs e)
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
                MessageBox.Show(rm.GetString("favoriteError"), rm.GetString("favoriteErrorCaption"),
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (var player in playersToMove)
                player.setFavorite(isFavoriting);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new RankingListsForm(comboBox.Text).Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = MessageBox.Show(rm.GetString("exitConfirm"), rm.GetString("exitConfirmCaption"),
                    MessageBoxButtons.OKCancel) == DialogResult.Cancel;
        }
    }
}