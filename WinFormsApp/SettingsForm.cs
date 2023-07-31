using ClassLibrary;

namespace WinFormsApp
{
    public partial class SettingsForm : Form
    {
        public const string fileName = "language_and_gender.txt";

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            if (!Settings.SettingsExist(fileName))
                return;
            string[] settings = Settings.LoadSettings(fileName);
            languageComboBox.Text = settings[0];
            genderComboBox.Text = settings[1];
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            if (languageComboBox.SelectedIndex == -1 || genderComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select valid options!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Settings.SaveSettings(fileName, languageComboBox.Text, genderComboBox.Text);
            Settings.GenderPath = genderComboBox.Text;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (ModifierKeys == Keys.None && keyData == Keys.Escape || keyData == Keys.Enter)
            {
                if (keyData == Keys.Escape)
                    cancelButton_Click(this, EventArgs.Empty);
                if (keyData == Keys.Enter)
                    confirmButton_Click(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
