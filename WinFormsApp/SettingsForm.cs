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
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void langComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void genderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
