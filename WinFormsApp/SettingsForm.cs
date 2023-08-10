using ClassLibrary;
using System.Resources;
using WinFormsApp.Properties;

namespace WinFormsApp
{
    public partial class SettingsForm : Form
    {
        private static readonly ResourceManager rm = new(typeof(Resources));
        private readonly bool isFirstStartup = false;

        public SettingsForm(bool isFirstStartup = false)
        {
            InitializeComponent();
            this.isFirstStartup = isFirstStartup;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            if (!UserSettings.SettingsExist()) return;
            string[] settings = UserSettings.LoadSettings();
            languageComboBox.SelectedIndex = int.Parse(settings[0]);
            championshipComboBox.SelectedIndex = int.Parse(settings[1]);
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            var indexes = new[] { languageComboBox, championshipComboBox }
                .Select(el => el.SelectedIndex.ToString()).ToList();
            if (indexes.Any(el => el == "-1"))
            {
                MessageBox.Show(rm.GetString("submitError"), rm.GetString("error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!isFirstStartup && AppSettings.ConfirmDialogsEnabled &&
                MessageBox.Show(rm.GetString("confirmSettings"), rm.GetString("confirmSettingsCaption"),
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;
            if (UserSettings.SettingsExist(length: 3))
                indexes.Add(UserSettings.LoadSettings()[2]);
            UserSettings.SaveSettings(settings: indexes.ToArray());
            DialogResult = DialogResult.OK;
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
