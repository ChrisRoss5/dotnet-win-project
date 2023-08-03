namespace WinFormsApp
{
    partial class PlayerUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            nameLabel = new Label();
            playerPictureBox = new PictureBox();
            detailsLabel = new Label();
            starPictureBox = new PictureBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            changePictureToolStripMenuItem = new ToolStripMenuItem();
            moveToFavoritesToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)playerPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)starPictureBox).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(59, 9);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(82, 20);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "nameLabel";
            nameLabel.MouseDown += PlayerUserControl_MouseDown;
            // 
            // playerPictureBox
            // 
            playerPictureBox.Cursor = Cursors.Hand;
            playerPictureBox.Image = Properties.Resources.player_icon2;
            playerPictureBox.Location = new Point(3, 9);
            playerPictureBox.Name = "playerPictureBox";
            playerPictureBox.Size = new Size(50, 50);
            playerPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            playerPictureBox.TabIndex = 1;
            playerPictureBox.TabStop = false;
            playerPictureBox.Click += playerPictureBox_Click;
            // 
            // detailsLabel
            // 
            detailsLabel.AutoSize = true;
            detailsLabel.Location = new Point(59, 35);
            detailsLabel.Name = "detailsLabel";
            detailsLabel.Size = new Size(89, 20);
            detailsLabel.TabIndex = 2;
            detailsLabel.Text = "detailsLabel";
            detailsLabel.MouseDown += PlayerUserControl_MouseDown;
            // 
            // starPictureBox
            // 
            starPictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            starPictureBox.Cursor = Cursors.Hand;
            starPictureBox.Image = Properties.Resources.star_icon;
            starPictureBox.Location = new Point(164, 9);
            starPictureBox.Name = "starPictureBox";
            starPictureBox.Size = new Size(20, 20);
            starPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            starPictureBox.TabIndex = 3;
            starPictureBox.TabStop = false;
            starPictureBox.Visible = false;
            starPictureBox.Click += starPictureBox_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { changePictureToolStripMenuItem, moveToFavoritesToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(195, 52);
            contextMenuStrip1.Closed += contextMenuStrip1_Closed;
            contextMenuStrip1.Opened += contextMenuStrip1_Opened;
            // 
            // changePictureToolStripMenuItem
            // 
            changePictureToolStripMenuItem.Name = "changePictureToolStripMenuItem";
            changePictureToolStripMenuItem.Size = new Size(194, 24);
            changePictureToolStripMenuItem.Text = "Change picture";
            // 
            // moveToFavoritesToolStripMenuItem
            // 
            moveToFavoritesToolStripMenuItem.Name = "moveToFavoritesToolStripMenuItem";
            moveToFavoritesToolStripMenuItem.Size = new Size(194, 24);
            moveToFavoritesToolStripMenuItem.Text = "Move to favorites";
            moveToFavoritesToolStripMenuItem.Click += moveToFavoritesToolStripMenuItem_Click;
            // 
            // PlayerUserControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            BorderStyle = BorderStyle.FixedSingle;
            ContextMenuStrip = contextMenuStrip1;
            Controls.Add(starPictureBox);
            Controls.Add(detailsLabel);
            Controls.Add(playerPictureBox);
            Controls.Add(nameLabel);
            Name = "PlayerUserControl";
            Size = new Size(198, 68);
            Load += PlayerUserControl_Load;
            MouseDown += PlayerUserControl_MouseDown;
            ((System.ComponentModel.ISupportInitialize)playerPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)starPictureBox).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label nameLabel;
        private PictureBox playerPictureBox;
        private Label detailsLabel;
        private PictureBox starPictureBox;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem changePictureToolStripMenuItem;
        private ToolStripMenuItem moveToFavoritesToolStripMenuItem;
    }
}
