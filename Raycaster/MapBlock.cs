using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raycaster
{
    public class MapBlock
    {
        public RectangleF size;
        public string type;
        public float angle = 0;
        public MapBlock(float x, float y, float width, float height, string type)
        {
            size = new RectangleF(x, y, width, height);
            this.type = type;
        }

        public MapBlock(float x, float y, float width, float height, string type, float angle)
        {
            size = new RectangleF(x, y, width, height);
            this.type = type;
            this.angle = angle;
        }
    }
}
