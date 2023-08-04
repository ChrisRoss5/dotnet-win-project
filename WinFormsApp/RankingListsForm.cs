using ClassLibrary;
using ClassLibrary.Models;
using ClassLibrary.Repo;
using ClassLibrary.Services;
using System.Data;
using System.Diagnostics.Metrics;
using System.Drawing.Printing;
using System.Resources;
using System.Windows.Forms;
using WinFormsApp.Properties;

namespace WinFormsApp
{
    public partial class RankingListsForm : Form
    {
        private readonly IWorldCupService worldCupService = new WorldCupService(new RestApiRepo());
        private readonly ResourceManager rm = new(typeof(Resources));
        private readonly string countryCode;

        public RankingListsForm(string team)
        {
            InitializeComponent();
            Text = $"{rm.GetString("rankingListTitle")} - {team}";
            countryCode = team.Split('(', ')')[1];
        }

        private void RankingListsForm_Load(object sender, EventArgs e)
        {
            LoadPlayerRankings(panel1, TypeOfEvent.Goal);
            LoadPlayerRankings(panel2, TypeOfEvent.YellowCard);
            LoadMatchRankings(panel3);
        }

        private async void LoadPlayerRankings(Panel panel, TypeOfEvent _event)
        {
            var list = await worldCupService.GetPlayersWithEventCount(countryCode, _event);
            var playerImagesPath = Settings.SolutionFolderPath + "/PlayerImages/";
            int rank = 1, lastGoals = list[0].Value;
            foreach (var (player, goals) in list)
            {
                var files = Directory.GetFiles(playerImagesPath, player.Name + ".*");
                var image = files.Length > 0 ? Image.FromFile(files[0]) : Resources.player_icon2;
                var listItem = CreateListItem(
                    goals < lastGoals ? ++rank : rank,
                    new PictureBox
                    {
                        Image = image,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Size = new Size(50, 50),
                        Location = new Point(10, 10),
                    },
                    CreateItemLabel($"{player.Name}", new Point(60, 10), true),
                    CreateItemLabel($"{rm.GetString(_event.ToString())}: {goals}", new Point(60, 35))
                );
                panel.Controls.Add(listItem);
                listItem.BringToFront();
                lastGoals = goals;
            }
        }

        private async void LoadMatchRankings(Panel panel)
        {
            var list = (await worldCupService.GetMatches(countryCode))
                .OrderByDescending(m => m.Attendance).ToList();
            int rank = 1, lastAttendance = (int)list[0].Attendance;
            panel.Controls.AddRange(list.Select((m, i) => CreateListItem(
                m.Attendance < lastAttendance ? ++rank : rank,
                CreateItemLabel($"{m.HomeTeamCountry} VS {m.AwayTeamCountry}", new Point(20, 10), true),
                CreateItemLabel($"{m.Location}", new Point(20, 35)),
                CreateItemLabel($"{m.Attendance} {rm.GetString("visitors")}", new Point(20, 60))
            )).Reverse().ToArray());
        }

        private Panel CreateListItem(int i, params Control[] controls)
        {
            var panel = new Panel
            {
                AutoSize = true,
                Dock = DockStyle.Top,
                Padding = new Padding(0, 0, 0, 10),
                BorderStyle = BorderStyle.FixedSingle,
                Controls =
                {
                    new Label
                    {
                        Text = $"{i}.",
                        Font = new Font(Font, FontStyle.Bold),
                        Location = new Point(0, 0),
                        ForeColor = Color.DarkRed,
                        BackColor = Color.Transparent,
                        AutoSize = true,
                    }
                }
            };
            panel.Controls.AddRange(controls);
            return panel;
        }

        private Label CreateItemLabel(string text, Point location, bool bold = false)
        {
            return new Label
            {
                Text = text,
                Font = new Font(Font, bold ? FontStyle.Bold : FontStyle.Regular),
                Location = location,
                AutoSize = true,
            };
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            printDialog1.Document.DocumentName = Text;
            printDialog1.Document.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            printPanel.BackColor = Color.White;
            Bitmap controlBitmap = new Bitmap(printPanel.Width, printPanel.Height);
            printPanel.DrawToBitmap(controlBitmap, new Rectangle(
                0, 0, printPanel.Width, printPanel.Height));
            printPanel.BackColor = DefaultBackColor;
            float aspectRatio = (float)controlBitmap.Width / controlBitmap.Height;
            int newHeight = e.MarginBounds.Height;
            int newWidth = (int)(newHeight * aspectRatio);
            int x = e.MarginBounds.Left + (e.MarginBounds.Width - newWidth) / 2;
            int y = e.MarginBounds.Top + (e.MarginBounds.Height - newHeight) / 2;
            e.Graphics!.DrawImage(controlBitmap, x, y, newWidth, newHeight);
        }
    }
}
