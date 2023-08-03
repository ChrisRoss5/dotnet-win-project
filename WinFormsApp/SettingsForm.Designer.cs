namespace WinFormsApp
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            label1 = new Label();
            label2 = new Label();
            languageComboBox = new ComboBox();
            championshipComboBox = new ComboBox();
            confirmButton = new Button();
            cancelButton = new Button();
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
            label2.Name = "label2";
            // 
            // languageComboBox
            // 
            resources.ApplyResources(languageComboBox, "languageComboBox");
            languageComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            languageComboBox.Items.AddRange(new object[] { resources.GetString("languageComboBox.Items"), resources.GetString("languageComboBox.Items1") });
            languageComboBox.Name = "languageComboBox";
            // 
            // championshipComboBox
            // 
            resources.ApplyResources(championshipComboBox, "championshipComboBox");
            championshipComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            championshipComboBox.FormattingEnabled = true;
            championshipComboBox.Items.AddRange(new object[] { resources.GetString("championshipComboBox.Items"), resources.GetString("championshipComboBox.Items1") });
            championshipComboBox.Name = "championshipComboBox";
            // 
            // confirmButton
            // 
            resources.ApplyResources(confirmButton, "confirmButton");
            confirmButton.Name = "confirmButton";
            confirmButton.UseVisualStyleBackColor = true;
            confirmButton.Click += confirmButton_Click;
            // 
            // cancelButton
            // 
            resources.ApplyResources(cancelButton, "cancelButton");
            cancelButton.Name = "cancelButton";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // SettingsForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(cancelButton);
            Controls.Add(confirmButton);
            Controls.Add(championshipComboBox);
            Controls.Add(languageComboBox);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            Load += SettingsForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox languageComboBox;
        private ComboBox championshipComboBox;
        private Button confirmButton;
        private Button cancelButton;
    }
}