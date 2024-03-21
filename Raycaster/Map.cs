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

        public Map(int[] mapInd, int mapX, int mapY, int mapS, int mapAdjustX, int mapAdjustY)
        {
            this.mapX = mapX;
            this.mapY = mapY;
            this.mapS = mapS;
            this.mapAdjustX = mapAdjustX;
            this.mapAdjustY = mapAdjustY;
            DrawMap(mapInd);
        }

        private void DrawMap(int[] mapInd)
        {
            mapPoints = new MapBlock[mapInd.Length];

            for (int i = 0; i < mapY; i++)
            {
                for (int j = 0; j < mapX; j++)
                {
                    if (mapInd[j + (i * mapX)] == 0)
                    {
                        mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "E");
                    }
                    else
                    {
                        mapPoints[j + (i * mapX)] = new MapBlock(mapAdjustX + (j * mapS) + (spacing / 2), mapAdjustY + (i * mapS) + (spacing / 2), mapS - spacing, mapS - spacing, "F");
                    }
                }
            }
        }
    }
}
