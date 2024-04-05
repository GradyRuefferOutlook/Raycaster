using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Raycaster
{
    internal class Ray
    {
        public PointF rayPoint = new PointF();
        public float distance, lineH;
        public int mapPoint;
        public string hOrV;
        public float rayAngle;
        public float ty;
        public float tyStep;
        public float tyOff = 0;
        public float tx;

        public Ray(PointF rayPoint, float distance, int mapPoint, string hOrV, float rayAngle)
        {
            this.rayPoint = rayPoint;
            this.distance = distance;
            this.mapPoint = mapPoint;
            this.hOrV = hOrV;
            this.rayAngle = rayAngle;
        }

        public void DrawWall(GameScreen gameScreen, int screenHeight)
        {
            lineH = (gameScreen.mapS * screenHeight) / distance;

            tyStep = 32 / lineH;

            if (lineH > screenHeight)
            {
                tyOff = (float)((lineH - 320) / 1.5);
                lineH = screenHeight;
            }
        }
    }
}
