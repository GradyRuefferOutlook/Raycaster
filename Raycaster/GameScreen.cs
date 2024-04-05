using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Raycaster
{
    public partial class GameScreen : UserControl
    {
        Player player;
        public Map mapW;
        public int mapX = 15, mapY = 10, mapS = 64;
        int mapScale = 4;
        int mapAdjustX;
        int mapAdjustY;

        SolidBrush RedBrush = new SolidBrush(Color.Maroon);
        SolidBrush BlackBrush = new SolidBrush(Color.FromArgb(255, 10, 10, 10));
        SolidBrush RedBrush2 = new SolidBrush(Color.FromArgb(255, 55, 0, 0));
        SolidBrush BlackBrush2 = new SolidBrush(Color.FromArgb(255, 0, 0, 0));

        SolidBrush GreyBrush = new SolidBrush(Color.Gray);
        SolidBrush GreyBrush2 = new SolidBrush(Color.FromArgb(255, Color.Gray.R - 25, Color.Gray.G - 25, Color.Gray.B - 25));

        SolidBrush BrownBrush = new SolidBrush(Color.SaddleBrown);
        SolidBrush BrownBrush2 = new SolidBrush(Color.FromArgb(255, Color.SaddleBrown.R - 19, Color.SaddleBrown.G - 19, Color.SaddleBrown.B -19));

        SolidBrush TanBrush = new SolidBrush(Color.Tan);
        SolidBrush TanBrush2 = new SolidBrush(Color.FromArgb(255, Color.Tan.R - 25, Color.Tan.G - 25, Color.Tan.B - 25));

        SolidBrush PurpleBrush = new SolidBrush(Color.Purple);
        SolidBrush PurpleBrush2 = new SolidBrush(Color.FromArgb(255, Color.Purple.R - 25, Color.Purple.G, Color.Purple.B - 25));

        SolidBrush GoldBrush = new SolidBrush(Color.Gold);
        SolidBrush GoldBrush2 = new SolidBrush(Color.FromArgb(255, Color.Gold.R - 25, Color.Gold.G - 25, Color.Gold.B));
        public double[] mapP =
        {
            1,1,1,1,1,1,1,1,1,1,1,1,1,99.75,1,
            1,0,2,0,0,0,0,0,0,0,0,3,0,0,1,
            1,0,2,2,2,2,2,2,0,2,0,3,0,3,1,
            1,0,2,0,0,0,0,0,0,2,2,3,0,3,1,
            1,0,0,0,0,0,0,0,0,0,0,3,0,3,1,
            1,0,0,0,0,5,0,0,0,0,0,91,0,0,1,
            1,0,0,0,0,4,0,0,0,3,91,2,2,0,1,
            1,0,0,0,0,4,4,4,4,2,0,2,2,0,1,
            1,0,0,0,0,0,0,0,0,0,0,2,0,0,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,99,1
        };

        public double[] mapC =
        {
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,
            1,0,2,0,0,0,0,0,0,0,0,3,0,0,1,
            1,0,2,2,2,2,2,2,0,2,0,3,0,3,1,
            1,0,2,0,0,0,0,0,0,2,2,3,0,3,1,
            1,0,0,0,0,0,0,0,0,0,0,3,0,3,1,
            1,0,0,0,0,1,0,0,0,0,0,1,0,0,1,
            1,0,0,0,0,1,0,0,0,1,1,2,2,0,1,
            1,0,0,0,0,1,1,1,1,1,0,2,2,0,1,
            1,0,0,0,0,0,0,0,0,0,0,2,0,0,1,
            1,1,1,1,1,1,1,1,1,1,1,1,1,1,1
        };

        public int[] BrickTexture =
        {
            0,0,0,0,0,0,0,0,          0,0,0,0,0,0,0,0,               0,0,0,0,0,0,0,0,           0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0,          0,1,1,1,1,1,1,0,               0,1,1,1,1,1,1,0,           0,1,1,1,1,1,1,0,
            0,1,1,1,1,1,1,0,          0,1,1,1,1,1,1,0,               0,1,1,1,1,1,1,0,           0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,          0,0,0,0,0,0,0,0,               0,0,0,0,0,0,0,0,           0,0,0,0,0,0,0,0,
            1,1,1,0,0,1,1,1,          1,1,1,0,0,1,1,1,               1,1,1,0,0,1,1,1,           1,1,1,0,0,1,1,1,
            1,1,1,0,0,1,1,1,          1,1,1,0,0,1,1,1,               1,1,1,0,0,1,1,1,           1,1,1,0,0,1,1,1,
            0,0,0,0,0,0,0,0,          0,0,0,0,0,0,0,0,               0,0,0,0,0,0,0,0,           0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0,          0,1,1,1,1,1,1,0,               0,1,1,1,1,1,1,0,           0,1,1,1,1,1,1,0,

            0,1,1,1,1,1,1,0,          0,1,1,1,1,1,1,0,               0,1,1,1,1,1,1,0,           0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,          0,0,0,0,0,0,0,0,               0,0,0,0,0,0,0,0,           0,0,0,0,0,0,0,0,
            1,1,1,0,0,1,1,1,          1,1,1,0,0,1,1,1,               1,1,1,0,0,1,1,1,           1,1,1,0,0,1,1,1,
            1,1,1,0,0,1,1,1,          1,1,1,0,0,1,1,1,               1,1,1,0,0,1,1,1,           1,1,1,0,0,1,1,1,
            0,0,0,0,0,0,0,0,          0,0,0,0,0,0,0,0,               0,0,0,0,0,0,0,0,           0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0,          0,1,1,1,1,1,1,0,               0,1,1,1,1,1,1,0,           0,1,1,1,1,1,1,0,
            0,1,1,1,1,1,1,0,          0,1,1,1,1,1,1,0,               0,1,1,1,1,1,1,0,           0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,          0,0,0,0,0,0,0,0,               0,0,0,0,0,0,0,0,           0,0,0,0,0,0,0,0,

            1,1,1,0,0,1,1,1,          1,1,1,0,0,1,1,1,               1,1,1,0,0,1,1,1,           1,1,1,0,0,1,1,1,
            1,1,1,0,0,1,1,1,          1,1,1,0,0,1,1,1,               1,1,1,0,0,1,1,1,           1,1,1,0,0,1,1,1,
            0,0,0,0,0,0,0,0,          0,0,0,0,0,0,0,0,               0,0,0,0,0,0,0,0,           0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0,          0,1,1,1,1,1,1,0,               0,1,1,1,1,1,1,0,           0,1,1,1,1,1,1,0,
            0,1,1,1,1,1,1,0,          0,1,1,1,1,1,1,0,               0,1,1,1,1,1,1,0,           0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,          0,0,0,0,0,0,0,0,               0,0,0,0,0,0,0,0,           0,0,0,0,0,0,0,0,
            1,1,1,0,0,1,1,1,          1,1,1,0,0,1,1,1,               1,1,1,0,0,1,1,1,           1,1,1,0,0,1,1,1,
            1,1,1,0,0,1,1,1,          1,1,1,0,0,1,1,1,               1,1,1,0,0,1,1,1,           1,1,1,0,0,1,1,1,

            0,0,0,0,0,0,0,0,          0,0,0,0,0,0,0,0,               0,0,0,0,0,0,0,0,           0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0,          0,1,1,1,1,1,1,0,               0,1,1,1,1,1,1,0,           0,1,1,1,1,1,1,0,
            0,1,1,1,1,1,1,0,          0,1,1,1,1,1,1,0,               0,1,1,1,1,1,1,0,           0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,          0,0,0,0,0,0,0,0,               0,0,0,0,0,0,0,0,           0,0,0,0,0,0,0,0,
            1,1,1,0,0,1,1,1,          1,1,1,0,0,1,1,1,               1,1,1,0,0,1,1,1,           1,1,1,0,0,1,1,1,
            1,1,1,0,0,1,1,1,          1,1,1,0,0,1,1,1,               1,1,1,0,0,1,1,1,           1,1,1,0,0,1,1,1,
            0,0,0,0,0,0,0,0,          0,0,0,0,0,0,0,0,               0,0,0,0,0,0,0,0,           0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0,          0,1,1,1,1,1,1,0,               0,1,1,1,1,1,1,0,           0,1,1,1,1,1,1,0,
        };

        public int[] BlockTexture =
        {
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0,
            0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0,
            0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0,
            0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,

            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0,
            0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0,
            0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0,
            0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,

            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0,
            0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0,
            0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0,
            0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,

            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0,
            0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0,
            0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0, 0,1,1,0,0,1,1,0,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0,
            0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0, 0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0
        };

        public int[] WoodTexture =
       {
            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,

            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,

            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,

            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,1,0,
        };

        public int[] ErrorTexture =
        {
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,

            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,

            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,


            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1,

        };

        public int[] DoorTexture =
        {
            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,0,
            0,1,2,2,2,1,1,1, 1,1,2,2,2,2,2,1, 1,2,2,2,2,2,1,1, 1,1,1,2,2,2,1,0,
            0,1,2,2,2,1,0,0, 0,1,2,2,2,2,2,1, 1,2,2,2,2,2,1,0, 0,0,1,2,2,2,1,0,
            0,1,2,2,2,1,0,0, 0,1,2,2,2,2,2,1, 1,2,2,2,2,2,1,0, 0,0,1,2,2,2,1,0,
            0,1,2,2,2,1,0,0, 0,1,2,2,2,2,2,1, 1,2,2,2,2,2,1,0, 0,0,1,2,2,2,1,0,
            0,1,2,2,2,1,1,1, 1,1,2,1,1,1,2,1, 1,2,1,1,1,2,1,1, 1,1,1,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,2,1,2,2,1, 1,2,2,1,2,2,2,2, 2,2,2,2,2,2,1,0,

            0,1,2,2,2,2,2,2, 2,2,2,1,2,1,2,1, 1,2,1,2,1,2,2,2, 2,2,2,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,1,1,1,2,1, 1,2,1,1,1,2,2,2, 2,2,2,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,2,2,2,2,1, 1,2,2,2,2,2,2,2, 2,2,2,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,2,2,2,2,1, 1,2,2,2,2,2,2,2, 2,2,2,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,2,2,2,2,1, 1,2,2,2,2,2,2,2, 2,2,2,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,2,2,2,2,1, 1,2,2,2,2,2,2,2, 2,2,2,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,2,2,2,2,1, 1,2,2,2,2,2,2,2, 2,2,2,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,2,2,2,2,1, 1,2,2,2,2,2,2,2, 2,2,2,2,2,2,1,0,

            0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,1, 1,1,1,1,1,1,1,0,
            0,1,2,2,2,1,1,1, 1,1,2,2,2,2,2,1, 1,2,2,2,2,2,1,1, 1,1,1,2,2,2,1,0,
            0,1,2,2,2,1,0,0, 0,1,2,2,2,2,2,1, 1,2,2,2,2,2,1,0, 0,0,1,2,2,2,1,0,
            0,1,2,2,2,1,0,0, 0,1,2,2,2,2,2,1, 1,2,2,2,2,2,1,0, 0,0,1,2,2,2,1,0,
            0,1,2,2,2,1,0,0, 0,1,2,2,2,2,2,1, 1,2,2,2,2,2,1,0, 0,0,1,2,2,2,1,0,
            0,1,2,2,2,1,1,1, 1,1,2,1,1,1,2,1, 1,2,1,1,1,2,1,1, 1,1,1,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,2,1,2,2,1, 1,2,2,1,2,2,2,2, 2,2,2,2,2,2,1,0,

            0,1,2,2,2,2,2,2, 2,2,2,1,2,1,2,1, 1,2,1,2,1,2,2,2, 2,2,2,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,1,1,1,2,1, 1,2,1,1,1,2,2,2, 2,2,2,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,2,2,2,2,1, 1,2,2,2,2,2,2,2, 2,2,2,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,2,2,2,2,1, 1,2,2,2,2,2,2,2, 2,2,2,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,2,2,2,2,1, 1,2,2,2,2,2,2,2, 2,2,2,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,2,2,2,2,1, 1,2,2,2,2,2,2,2, 2,2,2,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,2,2,2,2,1, 1,2,2,2,2,2,2,2, 2,2,2,2,2,2,1,0,
            0,1,2,2,2,2,2,2, 2,2,2,2,2,2,2,1, 1,2,2,2,2,2,2,2, 2,2,2,2,2,2,1,0,
        };

        public GameScreen()
        {
            InitializeComponent();
            player = new Player(Convert.ToSingle(mapS * 1.5), Convert.ToSingle(mapS * 1.5));
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            this.Size = new Size(Form1.width, Form1.height);

            mapAdjustX = this.Width - (mapX * (mapS + mapScale) / mapScale);
            //mapAdjustY = this.Height - (mapY * mapS / mapScale);
            mapAdjustY = 10;
            mapW = new Map(mapP, mapX, mapY, mapS / mapScale, mapAdjustX, mapAdjustY);

            GameOp.Enabled = true;
        }

        private void GameOp_Tick(object sender, EventArgs e)
        {
            player.Move(this);

            if (player.INT)
            {
                player.InteractH(this);
                player.InteractV(this);
            }

            if (player.hold.Count > 0)
            {
                player.CloseDoor(this);
            }

            Refresh();
        }

        private void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < player.rays.Count; i++)
            {
                player.rays[i].ty = player.rays[i].tyOff * player.rays[i].tyStep;

                if (player.rays[i].hOrV == "H")
                {
                    player.rays[i].tx = (int)(player.rays[i].rayPoint.X / 2) % 32;

                    if (player.rays[i].rayAngle > 180)
                    {
                        player.rays[i].tx = 31 - player.rays[i].tx;
                    }
                }
                else if (player.rays[i].hOrV == "V")
                {
                    player.rays[i].tx = (int)(player.rays[i].rayPoint.Y / 2) % 32;

                    if (player.rays[i].rayAngle > 90 && player.rays[i].rayAngle < 270)
                    {
                        player.rays[i].tx = 31 - player.rays[i].tx;
                    }
                }

                //This Adjusts the scale of the texture
                //For fun, although it will run slow and the doors will look odd, change it to one to se more detailed walls (WARNING: This will make it run extremelt=y slow)
                int jInc = 2;

                for (int j = 0; j < player.rays[i].lineH; j += jInc)
                {
                    //All of these statements following work the same way
                    //First, determine which texture is being drawn based off the wall that the ray hits
                    //Second, find the texture's y piece and add the x offset to find the texture pixel to draw
                    //Third, determine the colour ased off the numbering in the texture
                    //Finally, draw the pixel on the screen
                    //Rinse and repeat until the screen is filled
                    if (mapW.mapPoints[player.rays[i].mapPoint].type == "Br")
                    {
                        float c = BrickTexture[(((int)player.rays[i].ty * 32) % 1024) + (int)player.rays[i].tx];
                        if (c == 0)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(BlackBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(BlackBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                        else if (c == 1)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(RedBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(RedBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                    }

                    else if (mapW.mapPoints[player.rays[i].mapPoint].type == "Bl")
                    {
                        float c = BlockTexture[(((int)player.rays[i].ty * 32) % 1024) + (int)player.rays[i].tx];
                        if (c == 0)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(BlackBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(BlackBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                        else if (c == 1)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(GreyBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(GreyBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                    }

                    else if (mapW.mapPoints[player.rays[i].mapPoint].type == "WD")
                    {
                        float c = WoodTexture[(((int)player.rays[i].ty * 32) % 1024) + (int)player.rays[i].tx];
                        if (c == 0)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(BlackBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(BlackBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                        else if (c == 1)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(BrownBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(BrownBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                    }

                    else if (mapW.mapPoints[player.rays[i].mapPoint].type == "WT")
                    {
                        float c = WoodTexture[(((int)player.rays[i].ty * 32) % 1024) + (int)player.rays[i].tx];
                        if (c == 0)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(BlackBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(BlackBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                        else if (c == 1)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(TanBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(TanBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                    }

                    else if (mapW.mapPoints[player.rays[i].mapPoint].type == "Er")
                    {
                        float c = ErrorTexture[(((int)player.rays[i].ty * 32) % 1024) + (int)player.rays[i].tx];
                        if (c == 0)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(BlackBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(BlackBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                        else if (c == 1)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(PurpleBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(PurpleBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                    }

                    else if (mapW.mapPoints[player.rays[i].mapPoint].type == "DC")
                    {
                        float c = DoorTexture[(((int)player.rays[i].ty * 32) % 1024) + (int)player.rays[i].tx];
                        if (c == 0)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(BlackBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(BlackBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                        else if (c == 1)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(GreyBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(GreyBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                        else if (c == 2)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(BrownBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(BrownBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                    }

                    else if (mapW.mapPoints[player.rays[i].mapPoint].type == "DL")
                    {
                        float c = DoorTexture[(((int)player.rays[i].ty * 32) % 1024) + (int)player.rays[i].tx];
                        if (c == 0)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(GreyBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(GreyBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                        else if (c == 1)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(GoldBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(GoldBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                        else if (c == 2)
                        {
                            if (player.rays[i].hOrV == "H")
                            {
                                e.Graphics.FillRectangle(RedBrush, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                            else
                            {
                                e.Graphics.FillRectangle(RedBrush2, new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2) + (j), (player.screenWide / player.rays.Count), jInc));
                            }
                        }
                    }

                    player.rays[i].ty += player.rays[i].tyStep;
                }

                int lineOff = (this.Height / 2) - ((int)player.rays[i].lineH >> 1);

                for(int y = lineOff + (int)player.rays[i].lineH; y < this.Height; y++)
                {
                    float dy = y - (this.Height / 2);
                    
                }

                //if (player.rays[i].hOrV == "H")
                //{
                //    if (map.mapPoints[player.rays[i].mapPoint].type == "R")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.Red), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    if (map.mapPoints[player.rays[i].mapPoint].type == "O")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.Orange), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    if (map.mapPoints[player.rays[i].mapPoint].type == "Y")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    if (map.mapPoints[player.rays[i].mapPoint].type == "G")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.Green), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    else if (map.mapPoints[player.rays[i].mapPoint].type == "Bu")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.Blue), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    if (map.mapPoints[player.rays[i].mapPoint].type == "Pi")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.Pink), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    if (map.mapPoints[player.rays[i].mapPoint].type == "Pu")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.Purple), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    else if (map.mapPoints[player.rays[i].mapPoint].type == "Ba")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.Black), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    if (map.mapPoints[player.rays[i].mapPoint].type == "W")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.White), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //}
                //else
                //{
                //    if (map.mapPoints[player.rays[i].mapPoint].type == "R")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.DarkRed), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    if (map.mapPoints[player.rays[i].mapPoint].type == "O")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.DarkOrange), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    if (map.mapPoints[player.rays[i].mapPoint].type == "Y")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.Goldenrod), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    if (map.mapPoints[player.rays[i].mapPoint].type == "G")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.DarkGreen), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    else if (map.mapPoints[player.rays[i].mapPoint].type == "Bu")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.DarkBlue), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    if (map.mapPoints[player.rays[i].mapPoint].type == "Pi")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.LightPink), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    if (map.mapPoints[player.rays[i].mapPoint].type == "Pu")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.MediumPurple), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    else if (map.mapPoints[player.rays[i].mapPoint].type == "Ba")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.DarkSlateGray), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //    if (map.mapPoints[player.rays[i].mapPoint].type == "W")
                //    {
                //        e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new RectangleF((i * (player.screenWide / player.rays.Count)), (player.screenHeight / 2) - (player.rays[i].lineH / 2), (player.screenWide / player.rays.Count), player.rays[i].lineH));
                //    }
                //}
            }

            foreach (MapBlock point in mapW.mapPoints)
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
                e.Graphics.DrawLine(new Pen(Color.White, 3), (player.x / mapScale) + mapAdjustX, (player.y / mapScale) + mapAdjustY, (ray.rayPoint.X / mapScale) + mapAdjustX, (ray.rayPoint.Y / mapScale) + mapAdjustY);
            }

            e.Graphics.FillRectangle(new SolidBrush(Color.Yellow), new RectangleF(((player.x - 8) / mapScale) + mapAdjustX, ((player.y - 8) / mapScale) + mapAdjustY, 16 / mapScale, 16 / mapScale));
            e.Graphics.DrawLine(new Pen(Color.Yellow, 3 / mapScale), new PointF((player.x / mapScale) + mapAdjustX, (player.y / mapScale) + mapAdjustY), new PointF((player.x / mapScale) + Convert.ToSingle(player.dx * (5 / mapScale)) + mapAdjustX, (player.y / mapScale) + Convert.ToSingle(player.dy * (5 / mapScale)) + mapAdjustY));
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
                case Keys.E:
                    player.SetMove("INT", false);
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
                case Keys.E:
                    player.SetMove("INT", true);
                    break;
            }
        }
    }
}
