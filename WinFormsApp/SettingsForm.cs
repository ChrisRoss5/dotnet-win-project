using ClassLibrary;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Text.RegularExpressions;
using WinFormsApp.Properties;

namespace WinFormsApp
{
    public partial class SettingsForm : Form
    {
        public const string fileName = "language_and_championship.txt";
        private static readonly ResourceManager rm = new(typeof(Resources));

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            if (!Settings.SettingsExist(fileName))
                return;
            string[] settings = Settings.LoadSettings(fileName);
            languageComboBox.SelectedIndex = int.Parse(settings[0]);
            championshipComboBox.SelectedIndex = int.Parse(settings[1]);
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            var (languageIdx, championshipIdx) =
                (languageComboBox.SelectedIndex, championshipComboBox.SelectedIndex);
            if (languageIdx == -1 || championshipIdx == -1)
            {
                MessageBox.Show(rm.GetString("submitError"), rm.GetString("error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Settings.SaveSettings(fileName, languageIdx.ToString(), championshipIdx.ToString());
            this.DialogResult = DialogResult.OK;
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
