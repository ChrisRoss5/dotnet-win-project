using ClassLibrary;
using ClassLibrary.Models;
using System.Data;

namespace WinFormsApp
{
    public partial class PlayerUserControl : UserControl
    {
        public readonly Player player;
        private static readonly string playerImagesPath = Settings.SolutionFolderPath + "/PlayerImages/";

        public PlayerUserControl(Player player)
        {
            InitializeComponent();
            this.player = player;
            Dock = DockStyle.Top;
        }

        private void PlayerUserControl_Load(object sender, EventArgs e)
        {
            nameLabel.Text = $"{player.Name} ({player.ShirtNumber})";
            detailsLabel.Text = player.Position.ToString();
            if (player.Captain)
            {
                nameLabel.Font = new Font(Font, FontStyle.Bold);
                detailsLabel.Text += " | " + (Thread.CurrentThread.CurrentCulture.TextInfo.CultureName
                    == "en-US" ? "Captain" : "Kapetan");
            }
            var files = Directory.GetFiles(playerImagesPath, player.Name + ".*");
            if (files.Length > 0)
                playerPictureBox.Image = Image.FromFile(files[0]);
        }

        public void setFavorite(bool favorite)
        {
            var mainForm = (MainForm)Parent.Parent;
            mainForm.Controls[favorite ? "favoritesPanel" : "playersPanel"].Controls.Add(this);
            starPictureBox.Visible = favorite;
            contextMenuStrip1.Items[1].Text = favorite ? "Remove from favorites" : "Move to favorites";
            BackColor = DefaultBackColor;
            var favorites = mainForm.Controls["favoritesPanel"].Controls
                .Cast<PlayerUserControl>().Select(control => control.player.Name);
            Settings.SaveSettings($"favorite-{Settings.ChampionshipPath}-players.txt", favorites.ToArray()!);
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
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select a Image";
            fileDialog.Filter = "Images (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";
            if (!Directory.Exists(playerImagesPath))
                Directory.CreateDirectory(playerImagesPath);
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = fileDialog.FileName;
                string destination = playerImagesPath + player.Name + Path.GetExtension(filePath);
                File.Copy(filePath, destination);
                playerPictureBox.Image = new Bitmap(fileDialog.OpenFile());
            }
            else fileDialog.Dispose();
        }
    }
}
