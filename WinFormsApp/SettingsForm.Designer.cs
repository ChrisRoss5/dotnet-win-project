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
            label1 = new Label();
            label2 = new Label();
            langComboBox = new ComboBox();
            genderComboBox = new ComboBox();
            proceedButton = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(47, 53);
            label1.Name = "label1";
            label1.Size = new Size(118, 20);
            label1.TabIndex = 0;
            label1.Text = "Select language:";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(47, 97);
            label2.Name = "label2";
            label2.Size = new Size(103, 20);
            label2.TabIndex = 1;
            label2.Text = "Select gender:";
            label2.Click += label2_Click;
            // 
            // langComboBox
            // 
            langComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            langComboBox.FormattingEnabled = true;
            langComboBox.Items.AddRange(new object[] { "English", "Croatian" });
            langComboBox.Location = new Point(191, 50);
            langComboBox.Name = "langComboBox";
            langComboBox.Size = new Size(145, 28);
            langComboBox.TabIndex = 2;
            langComboBox.SelectedIndexChanged += langComboBox_SelectedIndexChanged;
            // 
            // genderComboBox
            // 
            genderComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            genderComboBox.FormattingEnabled = true;
            genderComboBox.Items.AddRange(new object[] { "Male", "Female" });
            genderComboBox.Location = new Point(191, 94);
            genderComboBox.Name = "genderComboBox";
            genderComboBox.Size = new Size(145, 28);
            genderComboBox.TabIndex = 3;
            genderComboBox.SelectedIndexChanged += genderComboBox_SelectedIndexChanged;
            // 
            // proceedButton
            // 
            proceedButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            proceedButton.Location = new Point(47, 158);
            proceedButton.Name = "proceedButton";
            proceedButton.Size = new Size(289, 35);
            proceedButton.TabIndex = 4;
            proceedButton.Text = "Proceed";
            proceedButton.UseVisualStyleBackColor = true;
            // 
            // InitForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(382, 253);
            Controls.Add(proceedButton);
            Controls.Add(genderComboBox);
            Controls.Add(langComboBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "InitForm";
            Text = "FIFA World Cup Settings";
            Load += InitForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox langComboBox;
        private ComboBox genderComboBox;
        private Button proceedButton;
    }
}