using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Raycaster
{
    public partial class GameScreen : UserControl
    {
        Player player = new Player(300, 300);
        public Map map;
        public int mapX = 15, mapY = 10, mapS = 64;
        int mapScale = 4;
        int mapAdjustX;
        int mapAdjustY;
        public int[] mapP =
        {
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,0,1,0,0,0,0,0,0,0,0,1,0,0,1,
            1,0,1,1,1,1,1,1,0,1,0,1,0,0,1,
            1,0,1,0,0,0,0,0,0,1,1,1,0,0,1,
            1,0,0,0,0,0,0,0,0,0,0,1,0,0,1,
            1,0,0,0,0,1,0,0,0,0,0,0,0,0,1,
            1,0,0,0,0,0,0,0,0,1,1,0,1,0,1,
            1,0,0,0,0,1,0,0,0,1,0,1,1,0,1,
            1,0,0,0,0,0,0,0,0,0,0,1,0,0,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1
        };

        public GameScreen()
        {
            InitializeComponent();
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            this.Size = new Size(Form1.width, Form1.height);

            mapAdjustX = this.Width - (mapX * (mapS + mapScale) / mapScale);
            //mapAdjustY = this.Height - (mapY * mapS / mapScale);
            mapAdjustY = 10;
            map = new Map(mapP, mapX, mapY, mapS / mapScale, mapAdjustX, mapAdjustY);

            GameOp.Enabled = true;
        }

        private void GameOp_Tick(object sender, EventArgs e)
        {
            player.Move();
            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < player.rays.Count; i++)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.Red), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
            }

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
            foreach (Ray ray in player.rays)
            {
                e.Graphics.DrawLine(new Pen(Color.Green, 3), (player.x / mapScale) + mapAdjustX, (player.y / mapScale) + mapAdjustY, (ray.rayPoint.X / mapScale) + mapAdjustX, (ray.rayPoint.Y / mapScale) + mapAdjustY);
            }

            e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), new RectangleF(((player.x - 8) / mapScale) + mapAdjustX, ((player.y - 8) / mapScale) + mapAdjustY, 16 / mapScale, 16 / mapScale));
            e.Graphics.DrawLine(new Pen(Color.Blue, 3 / mapScale), new PointF((player.x / mapScale) + mapAdjustX, (player.y / mapScale) + mapAdjustY ), new PointF((player.x / mapScale) + Convert.ToSingle(player.dx * (5 / mapScale)) + mapAdjustX, (player.y / mapScale) + Convert.ToSingle(player.dy * (5 / mapScale)) + mapAdjustY));
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
                case Keys.ShiftKey:
                    player.SetMove("SHF", false);
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
                case Keys.ShiftKey:
                    player.SetMove("SHF", true);
                    break;
            }
        }
    }
}
