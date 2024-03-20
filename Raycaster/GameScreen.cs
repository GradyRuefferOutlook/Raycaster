using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Raycaster
{
    public partial class GameScreen : UserControl
    {
        Player player = new Player(300, 300);
        public Map map;
        int mapX = 8, mapY = 8, mapS = 64;
        public int[] mapP =
        {
            1,1,1,1,1,1,1,1,
            1,0,1,0,0,0,0,1,
            1,0,1,0,0,0,0,1,
            1,0,1,0,0,0,0,1,
            1,0,0,0,0,0,0,1,
            1,0,0,0,0,1,0,1,
            1,0,0,0,0,0,0,1,
            1,1,1,1,1,1,1,1
        };

        public GameScreen()
        {
            InitializeComponent();
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            this.Size = new Size(Form1.width, Form1.height);

            map = new Map(mapP, mapX, mapY, mapS);

            GameOp.Enabled = true;
        }

        private void GameOp_Tick(object sender, EventArgs e)
        {
            player.Move();
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            foreach (MapBlock point in map.mapPoints)
            {
                if (point.type == "E")
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Black), point.size);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), point.size);
                }
            }

            player.drawRays(this);
            foreach (PointF ray in player.rays)
            {
                e.Graphics.DrawLine(new Pen(Color.Green, 3), player.x, player.y, ray.X, ray.Y);
            }

            e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), new RectangleF(player.x - 8, player.y - 8, 16, 16));
            e.Graphics.DrawLine(new Pen(Color.Blue, 3), new PointF(player.x, player.y), new PointF(player.x + Convert.ToSingle(player.dx * 4), player.y + Convert.ToSingle(player.dy * 4)));
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.SetMove("FWD", false);
                    break;
                case Keys.A:
                    player.SetMove("LFT", false);
                    break;
                case Keys.S:
                    player.SetMove("BCK", false);
                    break;
                case Keys.D:
                    player.SetMove("RHT", false);
                    break;
                case Keys.Left:
                    player.SetMove("TL", false);
                    break;
                case Keys.Right:
                    player.SetMove("TR", false);
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.SetMove("FWD", true);
                    break;
                case Keys.A:
                    player.SetMove("LFT", true);
                    break;
                case Keys.S:
                    player.SetMove("BCK", true);
                    break;
                case Keys.D:
                    player.SetMove("RHT", true);
                    break;
                case Keys.Left:
                    player.SetMove("TL", true);
                    break;
                case Keys.Right:
                    player.SetMove("TR", true);
                    break;
            }
        }
    }
}
