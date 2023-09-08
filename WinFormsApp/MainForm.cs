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
            if (!UserSettings.SettingsExist() && new SettingsForm(true).ShowDialog() != DialogResult.OK)
                Application.Exit();
            ApplySettingsAndInitalize(true);
            FormClosing += MainForm_FormClosing!;  // Potrebno ovdje umjesto u designeru jer bi se
                                                   // zbog InitializeComponent() ponavljalo svakom promjenom postavki
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (new SettingsForm().ShowDialog() == DialogResult.OK)
                ApplySettingsAndInitalize();
        }

        private void ApplySettingsAndInitalize(bool isStartup = false)
        {
            var settings = UserSettings.LoadSettings().Select(int.Parse).ToArray();
            (var language, var championship) = (settings[0], settings[1]);
            UserSettings.ChampionshipPath = championship == 0 ? "men" : "women";
            CultureInfo culture = new(language == 0 ? "en" : "hr");
            Thread.CurrentThread.CurrentCulture = culture;   // Globalizacija (vrijeme, datum, valuta)
            Thread.CurrentThread.CurrentUICulture = culture; // Lokalizacija (prijevodi)
            Controls.Clear();
            InitializeComponent();
            var enTitle = $"{(championship == 0 ? "Men's" : "Women's")} World Cup";
            var hrTitle = $"{(championship == 0 ? "Muško" : "Žensko")} Svjetsko prvenstvo";
            Text = $"FIFA {(language == 0 ? enTitle : hrTitle)} {(championship == 0 ? "2018" : "2019")}";
            if (!isStartup)
                MainForm_Load(this, EventArgs.Empty);
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            comboBoxLoaded = false;
            var teams = await worldCupService.GetTeams();
            comboBox.Items.AddRange(teams.Select(t => $"{t.Country} ({t.FifaCode})").ToArray());
            var fileName = $"favorite-{UserSettings.ChampionshipPath}-team.txt";
            if (UserSettings.SettingsExist(fileName))
            {
                comboBox.Text = UserSettings.LoadSettings(fileName)[0];
                LoadPlayers();
            }
            else button1.Enabled = false;
            comboBoxLoaded = true;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBoxLoaded) return;
            UserSettings.SaveSettings($"favorite-{UserSettings.ChampionshipPath}-team.txt", comboBox.Text);
            UserSettings.SaveSettings($"favorite-{UserSettings.ChampionshipPath}-players.txt", "");
            favoritesPanel.Controls.Clear();
            button1.Enabled = true;
            LoadPlayers();
        }

        private async void LoadPlayers()
        {
            var countryCode = comboBox.Text.Split('(', ')')[1];
            var players = await worldCupService.GetPlayers(countryCode);
            var playerControls = players.Select(p => new PlayerUserControl(p)).ToArray();
            playersPanel.Controls.Clear();
            playersPanel.Controls.AddRange(playerControls);
            var fileName = $"favorite-{UserSettings.ChampionshipPath}-players.txt";
            if (UserSettings.SettingsExist(fileName))
                foreach (var playerName in UserSettings.LoadSettings(fileName))
                    playerControls.FirstOrDefault(p => p.Player.Name == playerName)?.setFavorite(true);
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
            new RankingListsForm(worldCupService, comboBox.Text).Show();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AppSettings.ConfirmDialogsEnabled && e.CloseReason == CloseReason.UserClosing)
                e.Cancel = MessageBox.Show(rm.GetString("exitConfirm"), rm.GetString("exitConfirmCaption"),
                    MessageBoxButtons.OKCancel) == DialogResult.Cancel;
        }
    }
}