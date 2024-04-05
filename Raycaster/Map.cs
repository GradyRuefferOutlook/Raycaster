using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycaster
{
    public class Map
    {
        public MapBlock[] mapPoints;
        public int mapX, mapY, mapS, mapAdjustX, mapAdjustY;
        int spacing = 4;

        public Map(double[] mapInd, int mapX, int mapY, int mapS, int mapAdjustX, int mapAdjustY)
        {
            this.mapX = mapX;
            this.mapY = mapY;
            this.mapS = mapS;
            this.mapAdjustX = mapAdjustX;
            this.mapAdjustY = mapAdjustY;
            DrawMap(mapInd);
        }

        private void DrawMap(double[] mapInd)
        {
            mapPoints = new MapBlock[mapInd.Length];

            for (int i = 0; i < mapY; i++)
            {
                for (int j = 0; j < mapX; j++)
                {
                    if ((int)mapInd[j + (i * mapX)] == 0)
                    {
                        mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "E");
                    }
                    else if ((int)mapInd[j + (i * mapX)] == 1)
                    {
                        mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "Br");
                    }
                    else if ((int)mapInd[j + (i * mapX)] == 2)
                    {
                        mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "Bl");
                    }
                    else if ((int)mapInd[j + (i * mapX)] == 3)
                    {
                        mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "WD");
                    }
                    else if ((int)mapInd[j + (i * mapX)] == 4)
                    {
                        mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "WT");
                    }
                    //else if (mapInd[j + (i * mapX)] == 5)
                    //{
                    //    mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "Bu");
                    //}
                    //else if (mapInd[j + (i * mapX)] == 6)
                    //{
                    //    mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "Pi");
                    //}
                    //else if (mapInd[j + (i * mapX)] == 7)
                    //{
                    //    mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "Pu");
                    //}
                    //else if (mapInd[j + (i * mapX)] == 8)
                    //{
                    //    mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "Ba");
                    //}
                    //else if (mapInd[j + (i * mapX)] == 9)
                    //{
                    //    mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "W");
                    //}
                    else if ((int)mapInd[j + (i * mapX)] == 91)
                    {
                        mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "DC");
                    }
                    else if ((int)mapInd[j + (i * mapX)] == 99  || (int)mapInd[j + (i * mapX)] == 100)
                    {
                        mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "DL", (float)(Math.PI * (mapInd[j + (i * mapX)] % 99)) * 2);
                    }
                    else
                    {
                        mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "Er");
                    }
                }
            }
        }
    }
}
