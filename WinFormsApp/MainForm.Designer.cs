namespace WinFormsApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            comboBox = new ComboBox();
            menuStrip = new MenuStrip();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            button1 = new Button();
            label2 = new Label();
            label3 = new Label();
            favoritesPanel = new Panel();
            playersPanel = new Panel();
            label1 = new Label();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox
            // 
            comboBox.DropDownHeight = 300;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.FormattingEnabled = true;
            resources.ApplyResources(comboBox, "comboBox");
            comboBox.Name = "comboBox";
            comboBox.Sorted = true;
            comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(20, 20);
            menuStrip.Items.AddRange(new ToolStripItem[] { settingsToolStripMenuItem });
            resources.ApplyResources(menuStrip, "menuStrip");
            menuStrip.Name = "menuStrip";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(settingsToolStripMenuItem, "settingsToolStripMenuItem");
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // favoritesPanel
            // 
            favoritesPanel.AllowDrop = true;
            resources.ApplyResources(favoritesPanel, "favoritesPanel");
            favoritesPanel.BorderStyle = BorderStyle.FixedSingle;
            favoritesPanel.Name = "favoritesPanel";
            favoritesPanel.DragDrop += panel_DragDrop;
            favoritesPanel.DragEnter += panel_DragEnter;
            // 
            // playersPanel
            // 
            playersPanel.AllowDrop = true;
            resources.ApplyResources(playersPanel, "playersPanel");
            playersPanel.BorderStyle = BorderStyle.FixedSingle;
            playersPanel.Name = "playersPanel";
            playersPanel.DragDrop += panel_DragDrop;
            playersPanel.DragEnter += panel_DragEnter;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(comboBox);
            Controls.Add(menuStrip);
            Controls.Add(button1);
            Controls.Add(favoritesPanel);
            Controls.Add(playersPanel);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox;
        private MenuStrip menuStrip;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private Button button1;
        private Label label2;
        private Label label3;
        private Panel favoritesPanel;
        private Panel playersPanel;
        private Label label1;
    }
}