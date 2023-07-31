using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class PlayerUserControl : UserControl
    {
        private readonly Player player;

        public PlayerUserControl(Player player)
        {
            InitializeComponent();
            this.player = player;
            Dock = DockStyle.Top;
            if (player.Captain)
                BackColor = Color.LightYellow;
        }

        private void PlayerUserControl_Load(object sender, EventArgs e)
        {
            nameLabel.Text = player.Name;
            detailsLabel.Text = $"{player.ShirtNumber} | {player.Position}";
            if (player.Captain)
                detailsLabel.Text += Thread.CurrentThread.CurrentCulture.TextInfo.CultureName == "en-US" ? " | Captain" : " | Kapetan";
        }

        private void starPictureBox_Click(object sender, EventArgs e)
        {
            setFavorite(false);
        }

        public void setFavorite(bool favorite)
        {
            starPictureBox.Visible = favorite;
            Parent.Parent.Controls[favorite ? "favoritesPanel" : "playersPanel"].Controls.Add(this);
            contextMenuStrip1.Items[1].Text = favorite ? "Remove from favorites" : "Move to favorites";
            if (player.Captain)
                BackColor = Color.LightYellow;
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
            //DoDragDrop(this, DragDropEffects.Move);
        }

        private void PlayerUserControl_Click(object sender, EventArgs e)
        {
            BackColor = BackColor == DefaultBackColor ? Color.LightGray : DefaultBackColor;
        }
    }
}
