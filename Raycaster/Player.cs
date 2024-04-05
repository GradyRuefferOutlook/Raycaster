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
        public double angle = 0.5 * Math.PI, dx = 0, dy = 5;
        public bool LFT = false, RHT = false, FWD = false, BCK = false, TL = false, TR = false, SHF = false, INT = false;
        public float runScaler = 2, diagonalScaler = Convert.ToSingle(1.75);
        public List<Ray> rays = new List<Ray>();
        public int screenWide = 0;
        public int screenWideFactor = 8;
        public int screenHeight = 320;
        int windowScaler = 1;
        int scaler = 5;
        public List<double> hold = new List<double>();
        public Player(float x, float y)
        {
            this.x = x;
            this.y = y;
            dx = Math.Cos(angle) * 5;
            dy = Math.Sin(angle) * 5;
        }

        public float FixAngle(float a)
        {
            return (float)(a * (Math.PI / 180));
        }

        public void SetPosition(GameScreen gameScreen, int across, int down)
        {
            x = Convert.ToSingle((gameScreen.mapS * 1.5) + (64 * across));
            y = Convert.ToSingle((gameScreen.mapS * 1.5) + (64 * down));
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
                case "INT":
                    if (r)
                    {
                        INT = false;
                    }
                    else
                    {
                        INT = true;
                    }
                    break;
            }
        }

        public void InteractH(GameScreen gameScreen)
        {
            //Horizontal
            int xOffset = 0;
            int yOffset = 0;

            if (dx < 0)
            {
                xOffset = -25;
            }
            else
            {
                xOffset = 25;
            }

            if (dy < 0)
            {
                yOffset = -25 + 55;
            }
            else
            {
                yOffset = 65;
            }

            int gridPositionX = ((int)x + 32) / 64, gridPositionY = ((int)y - 32) / 64, gridPositionXPlusXOffset = ((int)x + xOffset) / 64, gridPositionYPlusYOffset = ((int)y - 32 + yOffset) / 64;

            if (gameScreen.mapW.mapPoints[gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset].type.Contains("D") && gameScreen.mapW.mapPoints[gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset].type != "DL")
            {
                if (gameScreen.mapP[gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset] != 0)
                {
                    hold.Add(gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset);
                    hold.Add(gameScreen.mapP[gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset]);
                    gameScreen.mapP[gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset] = 0;
                }
            }

            else if (gameScreen.mapW.mapPoints[gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset].type == "DL")
            {
                hold.Clear();
                angle = gameScreen.mapW.mapPoints[gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset].angle;
                dx = Math.Cos(angle) * 5;
                dy = Math.Sin(angle) * 5;
                SetPosition(gameScreen, 12, 7);
            }
        }

        public void InteractV(GameScreen gameScreen)
        {
            //Horizontal
            int xOffset = 0;
            int yOffset = 0;

            if (dx < 0)
            {
                xOffset = -25;
            }
            else
            {
                xOffset = 25;
            }

            if (dy < 0)
            {
                yOffset = -25;
            }
            else
            {
                yOffset = 65;
            }

            int gridPositionX = ((int)x + 32) / 64, gridPositionY = ((int)y - 32) / 64, gridPositionXPlusXOffset = ((int)x + xOffset) / 64, gridPositionYPlusYOffset = ((int)y - 32 + yOffset) / 64;

            if (gameScreen.mapW.mapPoints[gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset].type.Contains("D") && gameScreen.mapW.mapPoints[gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset].type != "DL")
            {
                if (gameScreen.mapP[gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset] != 0)
                {
                    hold.Add(gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset);
                    hold.Add(gameScreen.mapP[gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset]);
                    gameScreen.mapP[gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset] = 0;
                }
            }

            else if (gameScreen.mapW.mapPoints[gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset].type == "DL")
            {
                hold.Clear();
                angle = gameScreen.mapW.mapPoints[gridPositionYPlusYOffset * gameScreen.mapX + gridPositionXPlusXOffset].angle;
                dx = Math.Cos(angle) * 5;
                dy = Math.Sin(angle) * 5;
                SetPosition(gameScreen, 12, 7);
            }
        }

        public void CloseDoor(GameScreen gameScreen)
        {
            int xOffsetH = 0;
            int yOffsetH = 0;

            if (dx < 0)
            {
                xOffsetH = -25 - 64;
            }
            else
            {
                xOffsetH = 25;
            }

            if (dy < 0)
            {
                yOffsetH = -25 + 55;
            }
            else
            {
                yOffsetH = 65;
            }

            int gridPositionXH = ((int)x) / 64, gridPositionYH = ((int)y - 64) / 64, gridPositionXPlusXOffsetH = ((int)x + 32 + xOffsetH) / 64, gridPositionYPlusYOffsetH = ((int)y - 32 + yOffsetH) / 64;

            int xOffsetV = 0;
            int yOffsetV = 0;

            if (dx < 0)
            {
                xOffsetV = -25;
            }
            else
            {
                xOffsetV = -25;
            }

            if (dy < 0)
            {
                yOffsetV = -25;
            }
            else
            {
                yOffsetV = 65;
            }

            int gridPositionXV = ((int)x) / 64, gridPositionYV = ((int)y - 64) / 64, gridPositionXPlusXOffsetV = ((int)x + 32 + xOffsetV) / 64, gridPositionYPlusYOffsetV = ((int)y - 32 + yOffsetV) / 64;

            for (int i = 0; i < hold.Count; i += 2)
            {
                if (((gridPositionYH + 1) * gameScreen.mapX + gridPositionXH != hold[i] && gridPositionYPlusYOffsetH * gameScreen.mapX + gridPositionXPlusXOffsetH != hold[i]) && ((gridPositionYV + 1) * gameScreen.mapX + gridPositionXV != hold[i] && gridPositionYPlusYOffsetV * gameScreen.mapX + gridPositionXPlusXOffsetV != hold[i]))
                {
                    gameScreen.mapP[(int)hold[i]] = hold[i + 1];
                }
            }
        }

        public void Move(GameScreen gameScreen)
        {
            double diagonalMultiplier = diagonalScaler;

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

            int xOffset = 0, yOffset = 0;
            int xOffsetD = 0, yOffsetD = 0;
            if (dx < 0)
            {
                xOffset = -1;
                yOffsetD = -1;
            }

            else
            {
                xOffset = 1;
                yOffsetD = 1;
            }

            if (dy < 0)
            {
                yOffset = -1;
                xOffsetD = -1;
            }

            else
            {
                yOffset = 1;
                xOffsetD = 1;
            }

            int gridPositionX = Convert.ToInt32((x + 32) / 64), gridPositionXPlusXOffsetN = Convert.ToInt32((x + xOffset + 16) / 64), gridPositionXPlusXOffsetP = Convert.ToInt32((x + xOffset - 16) / 64), gridPositionXSubXOffsetN = Convert.ToInt32((x - xOffset + 48) / 64), gridPositionXSubXOffsetP = Convert.ToInt32((x - xOffset - 48) / 64);
            int gridPositionY = Convert.ToInt32((y - 32) / 64), gridPositionYPlusYOffsetN = Convert.ToInt32((y + yOffset + 16) / 64), gridPositionYPlusYOffsetP = Convert.ToInt32((y + yOffset - 16) / 64), gridPositionYSubYOffsetN = Convert.ToInt32((y - yOffset + 48) / 64), gridPositionYSubYOffsetP = Convert.ToInt32((y - yOffset - 48) / 64);

            if (!LFT && RHT)
            {
                if (SHF)
                {
                    if (FWD || BCK)
                    {
                        if (xOffsetD > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetN) - 1] == 0)
                            {
                                x -= Convert.ToSingle(dy / diagonalScaler * runScaler);
                            }
                        }
                        else if (xOffsetD < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetP)] == 0)
                            {
                                x -= Convert.ToSingle(dy / diagonalScaler * runScaler);
                            }
                        }

                        if (yOffsetD > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dx / diagonalScaler * runScaler);
                            }
                        }
                        else if (yOffsetD < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dx / diagonalScaler * runScaler);
                            }
                        }
                    }
                    else
                    {
                        if (xOffsetD > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetN) - 1] == 0)
                            {
                                x -= Convert.ToSingle(dy * runScaler);
                            }
                        }
                        else if (xOffsetD < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetP)] == 0)
                            {
                                x -= Convert.ToSingle(dy * runScaler);
                            }
                        }

                        if (yOffsetD > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dx * runScaler);
                            }
                        }
                        else if (yOffsetD < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dx * runScaler);
                            }
                        }
                    }
                }
                else
                {
                    if (FWD || BCK)
                    {
                        if (xOffsetD > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetN) - 1] == 0)
                            {
                                x -= Convert.ToSingle(dy/ diagonalScaler);
                            }
                        }
                        else if (xOffsetD < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetP)] == 0)
                            {
                                x -= Convert.ToSingle(dy / diagonalScaler);
                            }
                        }

                        if (yOffsetD > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dx / diagonalScaler);
                            }
                        }
                        else if (yOffsetD < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dx / diagonalScaler);
                            }
                        }
                    }
                    else
                    {
                        if (xOffsetD > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetN) - 1] == 0)
                            {
                                x -= Convert.ToSingle(dy);
                            }
                        }
                        else if (xOffsetD < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetP)] == 0)
                            {
                                x -= Convert.ToSingle(dy);
                            }
                        }

                        if (yOffsetD > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dx);
                            }
                        }
                        else if (yOffsetD < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dx);
                            }
                        }
                    }
                }
            }
            else if (!RHT && LFT)
            {
                if (SHF)
                {
                    if (FWD || BCK)
                    {
                        if (xOffsetD > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetP)] == 0)
                            {
                                x += Convert.ToSingle(dy / diagonalScaler * runScaler);
                            }
                        }
                        else if (xOffsetD < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetN) - 1] == 0)
                            {
                                x += Convert.ToSingle(dy / diagonalScaler * runScaler);
                            }
                        }

                        if (yOffsetD > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dx / diagonalScaler * runScaler);
                            }
                        }
                        else if (yOffsetD < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dx / diagonalScaler * runScaler);
                            }
                        }
                    }
                    else
                    {
                        if (xOffsetD > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetP)] == 0)
                            {
                                x += Convert.ToSingle(dy * runScaler);
                            }
                        }
                        else if (xOffsetD < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetN) - 1] == 0)
                            {
                                x += Convert.ToSingle(dy * runScaler);
                            }
                        }

                        if (yOffsetD > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dx * runScaler);
                            }
                        }
                        else if (yOffsetD < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dx * runScaler);
                            }
                        }
                    }
                }
                else
                {
                    if (FWD || BCK)
                    {
                        if (xOffsetD > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetP)] == 0)
                            {
                                x += Convert.ToSingle(dy / diagonalScaler);
                            }
                        }
                        else if (xOffsetD < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetN) - 1] == 0)
                            {
                                x += Convert.ToSingle(dy / diagonalScaler);
                            }
                        }

                        if (yOffsetD > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dx / diagonalScaler);
                            }
                        }
                        else if (yOffsetD < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dx / diagonalScaler);
                            }
                        }
                    }
                    else
                    {
                        if (xOffsetD > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetP)] == 0)
                            {
                                x += Convert.ToSingle(dy);
                            }
                        }
                        else if (xOffsetD < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetN) - 1] == 0)
                            {
                                x += Convert.ToSingle(dy);
                            }
                        }

                        if (yOffsetD > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dx);
                            }
                        }
                        else if (yOffsetD < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dx);
                            }
                        }
                    }
                }
            }

            if (!FWD && BCK)
            {
                if (SHF)
                {
                    if (LFT || RHT)
                    {
                        if (xOffset > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXSubXOffsetP)] == 0)
                            {
                                x -= Convert.ToSingle(dx / diagonalMultiplier * runScaler);
                            }
                        }
                        else if (xOffset < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXSubXOffsetN) - 1] == 0)
                            {
                                x -= Convert.ToSingle(dx / diagonalMultiplier * runScaler);
                            }
                        }

                        if (yOffset > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYSubYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dy / diagonalMultiplier * runScaler);
                            }
                        }
                        else if (yOffset < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYSubYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dy / diagonalMultiplier * runScaler);
                            }
                        }
                    }
                    else
                    {
                        if (xOffset > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXSubXOffsetP)] == 0)
                            {
                                x -= Convert.ToSingle(dx * runScaler);
                            }
                        }
                        else if (xOffset < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXSubXOffsetN) - 1] == 0)
                            {
                                x -= Convert.ToSingle(dx * runScaler);
                            }
                        }

                        if (yOffset > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYSubYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dy * runScaler);
                            }
                        }
                        else if (yOffset < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYSubYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dy * runScaler);
                            }
                        }
                    }
                }
                else
                {
                    if (LFT || RHT)
                    {
                        if (xOffset > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXSubXOffsetP)] == 0)
                            {
                                x -= Convert.ToSingle(dx / diagonalMultiplier);
                            }
                        }
                        else if (xOffset < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXSubXOffsetN) - 1] == 0)
                            {
                                x -= Convert.ToSingle(dx / diagonalMultiplier);
                            }
                        }

                        if (yOffset > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYSubYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dy / diagonalMultiplier);
                            }
                        }
                        else if (yOffset < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYSubYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dy / diagonalMultiplier);
                            }
                        }
                    }
                    else
                    {
                        if (xOffset > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXSubXOffsetP)] == 0)
                            {
                                x -= Convert.ToSingle(dx);
                            }
                        }
                        else if (xOffset < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXSubXOffsetN) - 1] == 0)
                            {
                                x -= Convert.ToSingle(dx);
                            }
                        }

                        if (yOffset > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYSubYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dy);
                            }
                        }
                        else if (yOffset < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYSubYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y -= Convert.ToSingle(dy);
                            }
                        }
                    }
                }
            }
            else if (!BCK && FWD)
            {
                if (SHF)
                {
                    if (LFT || RHT)
                    {
                        if (xOffset > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetP)] == 0)
                            {
                                x += Convert.ToSingle(dx / diagonalMultiplier * runScaler);
                            }
                        }
                        else if (xOffset < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetN) - 1] == 0)
                            {
                                x += Convert.ToSingle(dx / diagonalMultiplier * runScaler);
                            }
                        }

                        if (yOffset > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dy / diagonalMultiplier * runScaler);
                            }
                        }
                        else if (yOffset < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dy / diagonalMultiplier * runScaler);
                            }
                        }
                    }
                    else
                    {
                        if (xOffset > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetP)] == 0)
                            {
                                x += Convert.ToSingle(dx * runScaler);
                            }
                        }
                        else if (xOffset < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetN) - 1] == 0)
                            {
                                x += Convert.ToSingle(dx * runScaler);
                            }
                        }

                        if (yOffset > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dy * runScaler);
                            }
                        }
                        else if (yOffset < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dy * runScaler);
                            }
                        }
                    }

                }
                else
                {
                    if (LFT || RHT)
                    {
                        if (xOffset > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetP)] == 0)
                            {
                                x += Convert.ToSingle(dx / diagonalMultiplier);
                            }
                        }
                        else if (xOffset < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetN) - 1] == 0)
                            {
                                x += Convert.ToSingle(dx / diagonalMultiplier);
                            }
                        }

                        if (yOffset > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dy / diagonalMultiplier);
                            }
                        }
                        else if (yOffset < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dy / diagonalMultiplier);
                            }
                        }
                    }
                    else
                    {
                        if (xOffset > 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetP)] == 0)
                            {
                                x += Convert.ToSingle(dx);
                            }
                        }
                        else if (xOffset < 0)
                        {
                            if (gameScreen.mapP[(gridPositionY * gameScreen.mapX + gridPositionXPlusXOffsetN) - 1] == 0)
                            {
                                x += Convert.ToSingle(dx);
                            }
                        }
                        
                        if (yOffset > 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetP) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dy);
                            }
                        }
                        else if (yOffset < 0)
                        {
                            if (gameScreen.mapP[((gridPositionYPlusYOffsetN - 1) * gameScreen.mapX + gridPositionX) - 1] == 0)
                            {
                                y += Convert.ToSingle(dy);
                            }
                        }
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

            int depthOfField, mapX, mapY, mapP = 0;
            int tempMapPH = 0, tempMapPV = 0;

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
                    mapP = mapY * gameScreen.mapW.mapX + mapX;

                    while (mapP < 0)
                    {
                        mapP += gameScreen.mapP.Length;
                    }

                    if (mapP < gameScreen.mapW.mapX * gameScreen.mapW.mapY && gameScreen.mapP[mapP] != 0) //Check for hit wall
                    {
                        tempMapPH = mapP;
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
                    mapP = mapY * gameScreen.mapW.mapX + mapX;

                    while (mapP < 0)
                    {
                        mapP += gameScreen.mapP.Length;
                    }

                    if (mapP < gameScreen.mapW.mapX * gameScreen.mapW.mapY && gameScreen.mapP[mapP] != 0) //Check for hit wall
                    {
                        tempMapPV = mapP;
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
                string hOrV;

                if (disV < disM)
                {
                    rayX = vertX;
                    rayY = vertY;
                    disT = disV;
                    hOrV = "V";
                    mapP = tempMapPV;
                }

                else
                {
                    rayX = horiX;
                    rayY = horiY;
                    disT = disM;
                    hOrV = "H";
                    mapP = tempMapPH;
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

                rays.Add(new Ray(new PointF(rayX, rayY), disT, mapP, hOrV, rayAngle));

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
        }
    }
}
