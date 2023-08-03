using ClassLibrary;
using ClassLibrary.Models;
using ClassLibrary.Repo;
using System.Data;
using System.Drawing.Printing;
using System.Windows.Forms;
using WinFormsApp.Properties;

namespace WinFormsApp
{
    public partial class RankingListsForm : Form
    {
        public static readonly IRepo repo = RepoFactory.GetRepo();
        private readonly string country;

        public RankingListsForm(string country)
        {
            InitializeComponent();
            Text = $"Ranking lists - {country}";
            this.country = country;
        }

        private void RankingListsForm_Load(object sender, EventArgs e)
        {
            LoadPlayerRankings(panel1, TypeOfEvent.Goal);
            LoadPlayerRankings(panel2, TypeOfEvent.YellowCard);
            LoadMatchRankings(panel3);
        }

        private void LoadPlayerRankings(Panel panel, TypeOfEvent _event)
        {
            var list = repo.GetPlayersWithEventCount(country, _event);
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
                    CreateItemLabel($"{goals} {_event}", new Point(60, 35))
                );
                panel.Controls.Add(listItem);
                listItem.BringToFront();
                lastGoals = goals;
            }
        }

        private void LoadMatchRankings(Panel panel)
        {
            var list = repo.GetMatches(country).OrderByDescending(m => m.Attendance).ToList();
            int rank = 1, lastAttendance = (int)list[0].Attendance;
            panel.Controls.AddRange(list.Select((m, i) => CreateListItem(
                m.Attendance < lastAttendance ? ++rank : rank,
                CreateItemLabel($"{m.HomeTeamCountry} VS {m.AwayTeamCountry}", new Point(20, 10), true),
                CreateItemLabel($"{m.Location}", new Point(20, 35)),
                CreateItemLabel($"{m.Attendance} visitors", new Point(20, 60))
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
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            int totalHeight = 0;
            foreach (Control control in Controls)
                totalHeight = Math.Max(totalHeight, control.Bottom);
            Control controlToPrint = panel1;

            // Get the control's drawing area and convert it to a bitmap
            Bitmap controlBitmap = new Bitmap(controlToPrint.Width, controlToPrint.Height);
            controlToPrint.DrawToBitmap(controlBitmap, new Rectangle(0, 0, controlToPrint.Width, controlToPrint.Height));

            // Print the bitmap on the document
            e.Graphics.DrawImage(controlBitmap, e.MarginBounds.Location);
            /*Bitmap memoryImage = new(Size.Width, totalHeight);
            using (Graphics memoryGraphics = Graphics.FromImage(memoryImage))
            {
                memoryGraphics.Clear(Color.White);
                int scrollOffset = 0;
                for (int i = 0; scrollOffset < totalHeight; i++)
                {
                    VerticalScroll.Value = scrollOffset;
                    Update();
                    memoryGraphics.CopyFromScreen(Location.X, Location.Y + 25, 0, Size.Height * i, Size);
                    scrollOffset += ClientRectangle.Height;
                }

            }
            e.Graphics.ScaleTransform(0.5f, 0.5f);
            e.Graphics.DrawImage(memoryImage, 0, 0);*/
        }
    }
}
