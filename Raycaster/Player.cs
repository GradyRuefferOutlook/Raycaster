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
        public bool LFT = false, RHT = false, FWD = false, BCK = false, TL = false, TR = false, SHF = false;
        public float runScaler = 2;
        public List<Ray> rays = new List<Ray>();
        public int screenWide = 0;
        public int screenWideFactor = 8;
        public int screenHeight = 320;
        int windowScaler = 1;
        int scaler = 15;
        public Player(float x, float y)
        {
            this.x = x;
            this.y = y;
            dx = Math.Cos(angle) * 5;
            dy = Math.Sin(angle) * 5;
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
                case "SHF":
                    if (r)
                    {
                        SHF = false;
                    }
                    else
                    {
                        SHF = true;
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

            if (!LFT && RHT)
            {
                if (SHF)
                {
                    if (FWD || BCK)
                    {
                        x -= Convert.ToSingle(dy / 1.75 * runScaler);
                        y += Convert.ToSingle(dx / 1.75 * runScaler);
                    }
                    else
                    {
                        x -= Convert.ToSingle(dy * runScaler);
                        y += Convert.ToSingle(dx) * runScaler;
                    }
                }
                else
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
            }
            else if (!RHT && LFT)
            {
                if (SHF)
                {
                    if (FWD || BCK)
                    {
                        x += Convert.ToSingle(dy / 1.75 * runScaler);
                        y -= Convert.ToSingle(dx / 1.75 * runScaler);
                    }
                    else
                    {
                        x += Convert.ToSingle(dy * runScaler);
                        y -= Convert.ToSingle(dx * runScaler);
                    }
                }
                else
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
            }

            if (!FWD && BCK)
            {
                if (SHF)
                {
                    if (LFT || RHT)
                    {
                        x -= Convert.ToSingle(dx / 1.75 * runScaler);
                        y -= Convert.ToSingle(dy / 1.75 * runScaler);
                    }
                    else
                    {
                        x -= Convert.ToSingle(dx * runScaler);
                        y -= Convert.ToSingle(dy * runScaler);
                    }
                }
                else
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
            }
            else if (!BCK && FWD)
            {
                if (SHF)
                {
                    if (LFT || RHT)
                    {
                        x += Convert.ToSingle(dx / 1.75 * runScaler);
                        y += Convert.ToSingle(dy / 1.75 * runScaler);
                    }
                    else
                    {
                        x += Convert.ToSingle(dx * runScaler);
                        y += Convert.ToSingle(dy * runScaler);
                    }

                }
                else
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
        }

        private float dist(float ax, float ay, float bx, float by, float angle)
        {
            return Convert.ToSingle(Math.Sqrt(Math.Pow((bx - ax), 2) + Math.Pow((by - ay), 2)));
        }

        //To look further, change the range on depth of field
        public void drawRays(GameScreen gameScreen)
        {
            int depthChecker = 0;
            if (gameScreen.mapX > gameScreen.mapY)
            {
                depthChecker = gameScreen.mapX + 5;
            }
            else
            {
                depthChecker = gameScreen.mapX + 5;
            }

            int moveback = 30;

            rays.Clear();

            float rayAngle = Convert.ToSingle(angle - ((Math.PI / 180) * moveback)), rayX, rayY, xOffset = 0, yOffset = 0;

            if (rayAngle < 0)
            {
                rayAngle += Convert.ToSingle(2 * Math.PI);
            }

            else if (rayAngle > 2 * Math.PI)
            {
                rayAngle -= Convert.ToSingle(2 * Math.PI);
            }

            int depthOfField, mapX, mapY, mapP;

            for (int r = 0; r < 60 * scaler; r++)
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

                float disT = 0;

                if (disV < disM)
                {
                    rayX = vertX;
                    rayY = vertY;
                    disT = disV;
                }

                else
                {
                    rayX = horiX;
                    rayY = horiY;
                    disT = disM;
                }

                float ca = Convert.ToSingle(angle - rayAngle);

                if (ca < 0)
                {
                    ca += Convert.ToSingle(2 * Math.PI);
                }

                else if (ca > 2 * Math.PI)
                {
                    ca -= Convert.ToSingle(2 * Math.PI);
                }

                disT = Convert.ToSingle(disT * Math.Cos(ca));

                rays.Add(new Ray(new PointF(rayX, rayY), disT));

                screenHeight = gameScreen.Height;

                rays[r].DrawWall(gameScreen, screenHeight * windowScaler);

                rayAngle += Convert.ToSingle(Math.PI / (180 * scaler));

                if (rayAngle < 0)
                {
                    rayAngle += Convert.ToSingle(2 * Math.PI);
                }

                else if (rayAngle > 2 * Math.PI)
                {
                    rayAngle -= Convert.ToSingle(2 * Math.PI);
                }
            }

            screenWide = gameScreen.Width;
            //screenWide = (screenWideFactor / scaler) * rays.Count * windowScaler;
        }
    }
}
