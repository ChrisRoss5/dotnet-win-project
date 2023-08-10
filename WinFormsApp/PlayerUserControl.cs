using ClassLibrary;
using ClassLibrary.Models;
using System.Data;
using System.Globalization;

namespace WinFormsApp
{
    public partial class PlayerUserControl : UserControl
    {
        public Player Player { get; }

        public PlayerUserControl(Player player)
        {
            InitializeComponent();
            Dock = DockStyle.Top;
            Player = player;
        }

        private void PlayerUserControl_Load(object sender, EventArgs e)
        {
            nameLabel.Text = $"{Player.Name} ({Player.ShirtNumber})";
            detailsLabel.Text = Player.Position.ToString();
            if (Player.Captain)
            {
                nameLabel.Font = new Font(Font, FontStyle.Bold);
                detailsLabel.Text += " | " + (
                    CultureInfo.CurrentUICulture.Name == "en" ? "Captain" : "Kapetan");
            }
            var path = UserSettings.GetPlayerImagePath(Player.Name);
            if (path != "")
                playerPictureBox.Image = Image.FromFile(path);
        }

        public void setFavorite(bool favorite)
        {
            var mainForm = (MainForm)Parent.Parent;
            mainForm.Controls[favorite ? "favoritesPanel" : "playersPanel"].Controls.Add(this);
            starPictureBox.Visible = favorite;
            contextMenuStrip1.Items[1].Text = favorite ? "Remove from favorites" : "Move to favorites";
            BackColor = DefaultBackColor;
            var favorites = mainForm.Controls["favoritesPanel"].Controls
                .Cast<PlayerUserControl>().Select(control => control.Player.Name);
            UserSettings.SaveSettings($"favorite-{UserSettings.ChampionshipPath}-players.txt", favorites.ToArray()!);
        }

        private void starPictureBox_Click(object sender, EventArgs e)
        {
            setFavorite(false);
        }

        private void moveToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setFavorite(!starPictureBox.Visible);
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            BackColor = Color.LightGray;
        }

        private void contextMenuStrip1_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            BackColor = DefaultBackColor;
        }

        private void PlayerUserControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if ((ModifierKeys & Keys.Control) == Keys.Control)
                BackColor = BackColor == DefaultBackColor ? Color.LightGray : DefaultBackColor;
            else
                DoDragDrop(this, DragDropEffects.Move);
        }

        private void playerPictureBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new()
            {
                Title = "Select a Image",
                Filter = "Images (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };
            if (!Directory.Exists(UserSettings.PlayerImagesPath))
                Directory.CreateDirectory(UserSettings.PlayerImagesPath);
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = fileDialog.FileName;
                string destination = UserSettings.PlayerImagesPath + Player.Name + Path.GetExtension(filePath);
                File.Copy(filePath, destination);
                playerPictureBox.Image = new Bitmap(fileDialog.OpenFile());
            }
            else fileDialog.Dispose();
        }
    }
}
