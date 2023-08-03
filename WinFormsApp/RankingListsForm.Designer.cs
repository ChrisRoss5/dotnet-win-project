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
            printDialog2 = new PrintDialog();
            printPanel = new Panel();
            menuStrip1.SuspendLayout();
            printPanel.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(125, 3);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 0;
            label1.Text = "Golovi";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.FlatStyle = FlatStyle.Popup;
            label2.Location = new Point(436, 3);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 1;
            label2.Text = "Žuti kartoni";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(769, 3);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 2;
            label3.Text = "Posjetitelji";
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(0, 26);
            panel1.Name = "panel1";
            panel1.Size = new Size(310, 10);
            panel1.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.AutoSize = true;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Location = new Point(326, 26);
            panel2.Name = "panel2";
            panel2.Size = new Size(310, 10);
            panel2.TabIndex = 6;
            // 
            // panel3
            // 
            panel3.AutoSize = true;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Location = new Point(651, 26);
            panel3.Name = "panel3";
            panel3.Size = new Size(310, 10);
            panel3.TabIndex = 6;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { printToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1006, 28);
            menuStrip1.TabIndex = 7;
            menuStrip1.Text = "menuStrip1";
            // 
            // printToolStripMenuItem
            // 
            printToolStripMenuItem.Name = "printToolStripMenuItem";
            printToolStripMenuItem.Size = new Size(52, 24);
            printToolStripMenuItem.Text = "Ispis";
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
            // printDialog2
            // 
            printDialog2.UseEXDialog = true;
            // 
            // printPanel
            // 
            printPanel.AutoSize = true;
            printPanel.Controls.Add(label1);
            printPanel.Controls.Add(label2);
            printPanel.Controls.Add(label3);
            printPanel.Controls.Add(panel1);
            printPanel.Controls.Add(panel2);
            printPanel.Controls.Add(panel3);
            printPanel.Location = new Point(12, 40);
            printPanel.Name = "printPanel";
            printPanel.Padding = new Padding(0, 0, 0, 20);
            printPanel.Size = new Size(964, 59);
            printPanel.TabIndex = 8;
            // 
            // RankingListsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1006, 721);
            Controls.Add(menuStrip1);
            Controls.Add(printPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "RankingListsForm";
            Text = "RankingListsForm";
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
        private PrintDialog printDialog2;
        private Panel printPanel;
    }
}