namespace WinFormsApp
{
    partial class RankingListsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RankingListsForm));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            menuStrip1 = new MenuStrip();
            printToolStripMenuItem = new ToolStripMenuItem();
            printDocument1 = new System.Drawing.Printing.PrintDocument();
            printDialog1 = new PrintDialog();
            printPanel = new Panel();
            menuStrip1.SuspendLayout();
            printPanel.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.FlatStyle = FlatStyle.Popup;
            label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Name = "panel1";
            // 
            // panel2
            // 
            resources.ApplyResources(panel2, "panel2");
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Name = "panel2";
            // 
            // panel3
            // 
            resources.ApplyResources(panel3, "panel3");
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Name = "panel3";
            // 
            // menuStrip1
            // 
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { printToolStripMenuItem });
            menuStrip1.Name = "menuStrip1";
            // 
            // printToolStripMenuItem
            // 
            resources.ApplyResources(printToolStripMenuItem, "printToolStripMenuItem");
            printToolStripMenuItem.Name = "printToolStripMenuItem";
            printToolStripMenuItem.Click += printToolStripMenuItem_Click;
            // 
            // printDocument1
            // 
            printDocument1.PrintPage += printDocument1_PrintPage;
            // 
            // printDialog1
            // 
            printDialog1.UseEXDialog = true;
            // 
            // printPanel
            // 
            resources.ApplyResources(printPanel, "printPanel");
            printPanel.Controls.Add(label1);
            printPanel.Controls.Add(label2);
            printPanel.Controls.Add(label3);
            printPanel.Controls.Add(panel1);
            printPanel.Controls.Add(panel2);
            printPanel.Controls.Add(panel3);
            printPanel.Name = "printPanel";
            // 
            // RankingListsForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(menuStrip1);
            Controls.Add(printPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "RankingListsForm";
            Load += RankingListsForm_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            printPanel.ResumeLayout(false);
            printPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem printToolStripMenuItem;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private PrintDialog printDialog1;
        private Panel printPanel;
    }
}