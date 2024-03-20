using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycaster
{
    internal class Player
    {
        public float x, y;
        public double angle = 0.25 * Math.PI, dx = 0, dy = 5;
        public bool LFT = false, RHT = false, FWD = false, BCK = false, TL = false, TR = false;
        public List<PointF> rays = new List<PointF>();
        public Player(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetMove(string m, bool r)
        {
            switch (m)
            {
                case "FWD":
                    if (r)
                    {
                        FWD = false;
                    }
                    else
                    {
                        FWD = true;
                    }
                    break;
                case "BCK":
                    if (r)
                    {
                        BCK = false;
                    }
                    else
                    {
                        BCK = true;
                    }
                    break;
                case "LFT":
                    if (r)
                    {
                        LFT = false;
                    }
                    else
                    {
                        LFT = true;
                    }
                    break;
                case "RHT":
                    if (r)
                    {
                        RHT = false;
                    }
                    else
                    {
                        RHT = true;
                    }
                    break;
                case "TL":
                    if (r)
                    {
                        TL = false;
                    }
                    else
                    {
                        TL = true;
                    }
                    break;
                case "TR":
                    if (r)
                    {
                        TR = false;
                    }
                    else
                    {
                        TR = true;
                    }
                    break;
            }
        }

        public void Move()
        {
            if (TL && !TR)
            {
                angle -= 0.1;
                if (angle < 0)
                {
                    angle += 2 * Math.PI;
                }
                dx = Math.Cos(angle) * 5;
                dy = Math.Sin(angle) * 5;
            }
            else if (TR && !TL)
            {
                angle += 0.1;
                if (angle > 2 * Math.PI)
                {
                    angle -= 2 * Math.PI;
                }
                dx = Math.Cos(angle) * 5;
                dy = Math.Sin(angle) * 5;
            }

            if (LFT && !RHT)
            {
                if (FWD || BCK)
                {
                    x -= Convert.ToSingle(dy / 1.75);
                    y += Convert.ToSingle(dx / 1.75);
                }
                else
                {
                    x -= Convert.ToSingle(dy);
                    y += Convert.ToSingle(dx);
                }
            }
            else if (RHT && !LFT)
            {
                if (FWD || BCK)
                {
                    x += Convert.ToSingle(dy / 1.75);
                    y -= Convert.ToSingle(dx / 1.75);
                }
                else
                {
                    x += Convert.ToSingle(dy);
                    y -= Convert.ToSingle(dx);
                }
            }

            if (FWD && !BCK)
            {
                if (LFT || RHT)
                {
                    x -= Convert.ToSingle(dx / 1.75);
                    y -= Convert.ToSingle(dy / 1.75);
                }
                else
                {
                    x -= Convert.ToSingle(dx);
                    y -= Convert.ToSingle(dy);
                }
            }
            else if (BCK && !FWD)
            {
                if (LFT || RHT)
                {
                    x += Convert.ToSingle(dx / 1.75);
                    y += Convert.ToSingle(dy / 1.75);
                }
                else
                {
                    x += Convert.ToSingle(dx);
                    y += Convert.ToSingle(dy);
                }
            }
        }

        private float dist(float ax, float ay, float bx, float by, float angle)
        {
            return Convert.ToSingle(Math.Sqrt(Math.Pow((bx - ax), 2) + Math.Pow((by - ay), 2)));
        }

        //To look further, change the range on depth of field
        public void drawRays(GameScreen gameScreen)
        {
            int depthChecker = 8;

            rays.Clear();

            float rayAngle = Convert.ToSingle(angle), rayX, rayY, xOffset = 0, yOffset = 0;
            int depthOfField, mapX, mapY, mapP;

            for (int r = 0; r < 1; r++)
            {
                //Check Horizontal
                depthOfField = 0;

                float disM = 1000000, horiX = x, horiY = y;

                float aTan = Convert.ToSingle(-1 / Math.Tan(rayAngle));

                if (rayAngle > Math.PI) //Looking Up
                {
                    rayY = Convert.ToSingle((((int)y >> 6) << 6) - 0.0001);
                    rayX = (y - rayY) * aTan + x;
                    yOffset = -64;
                    xOffset = -yOffset * aTan;
                }

                else if (rayAngle < Math.PI) //Looking Down
                {
                    rayY = Convert.ToSingle((((int)y >> 6) << 6) + 64);
                    rayX = (y - rayY) * aTan + x;
                    yOffset = 64;
                    xOffset = -yOffset * aTan;
                }

                else //Looking Straight
                {
                    rayX = x;
                    rayY = y;
                    depthOfField = depthChecker;
                }

                while (depthOfField < depthChecker)
                {
                    mapX = (int)(rayX) >> 6;
                    mapY = (int)(rayY) >> 6;
                    mapP = mapY * gameScreen.map.mapX + mapX;

                    while (mapP < 0)
                    {
                        mapP += gameScreen.mapP.Length;
                    }

                    if (mapP < gameScreen.map.mapX * gameScreen.map.mapY && gameScreen.mapP[mapP] == 1) //Check for hit wall
                    {
                        horiX = rayX;
                        horiY = rayY;
                        disM = dist(x, y, horiX, horiY, rayAngle);
                        depthOfField = depthChecker;
                    }

                    else
                    {
                        rayX += xOffset;
                        rayY += yOffset;
                        depthOfField += 1;
                    }
                }

                //Check Vertical
                depthOfField = 0;

                float disV = 1000000, vertX = x, vertY = y;

                float nTan = Convert.ToSingle(-Math.Tan(rayAngle));

                if (rayAngle > Math.PI / 2 && rayAngle < 3 * Math.PI / 2) //Looking Left
                {
                    rayX = Convert.ToSingle((((int)x >> 6) << 6) - 0.0001);
                    rayY = (x - rayX) * nTan + y;
                    xOffset = -64;
                    yOffset = -xOffset * nTan;
                }

                else if (rayAngle < Math.PI / 2 || rayAngle > 3 * Math.PI / 2) //Looking Right
                {
                    rayX = Convert.ToSingle((((int)x >> 6) << 6) + 64);
                    rayY = (x - rayX) * nTan + y;
                    xOffset = 64;
                    yOffset = -xOffset * nTan;
                }

                else //Looking Up or Down
                {
                    rayX = x;
                    rayY = y;
                    depthOfField = depthChecker;
                }

                while (depthOfField < depthChecker)
                {
                    mapX = (int)(rayX) >> 6;
                    mapY = (int)(rayY) >> 6;
                    mapP = mapY * gameScreen.map.mapX + mapX;

                    while (mapP < 0)
                    {
                        mapP += gameScreen.mapP.Length;
                    }

                    if (mapP < gameScreen.map.mapX * gameScreen.map.mapY && gameScreen.mapP[mapP] == 1) //Check for hit wall
                    {
                        vertX = rayX;
                        vertY = rayY;
                        disV = dist(x, y, vertX, vertY, rayAngle);
                        depthOfField = depthChecker;
                    }

                    else
                    {
                        rayX += xOffset;
                        rayY += yOffset;
                        depthOfField += 1;
                    }
                }

                if (disV < disM)
                {
                    rayX = vertX;
                    rayY = vertY;
                }

                else
                {
                    rayX = horiX;
                    rayY = horiY;
                }

                rays.Add(new PointF(rayX, rayY));
            }
        }
    }
}
