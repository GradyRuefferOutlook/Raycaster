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

        public Ray(PointF rayPoint, float distance)
        {
            this.rayPoint = rayPoint;
            this.distance = distance;
        }

        public void DrawWall(GameScreen gameScreen, int screenHeight)
        {
            lineH = (gameScreen.mapS * screenHeight) / distance;

            if (lineH > screenHeight)
            {
                lineH = screenHeight;
            }
        }
    }
}
