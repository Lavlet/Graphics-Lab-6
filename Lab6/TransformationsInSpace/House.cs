using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransformationsInSpace
{
    class House
    {
        private Point[] _box = new Point[]
        {
            new Point(0, 0, 0),
            new Point(0, 0, 1),
            new Point(0, 1, 0),
            new Point(0, 1, 1),
            new Point(1, 0, 0),
            new Point(1, 0, 1),
            new Point(1, 1, 0),
            new Point(1, 1, 1)
        };

        public Point[] Box
        {
            get
            {
                return _box;
            }
            set
            {
                _box = value;
            }
        }


        public System.Drawing.Point[] GetBox(int scale, int centerX, int centerY)
        {
            var points = ProjectionTool.ToArray(_box);
            var projection = ProjectionTool.Kavalie(points, 1, 4);

            return ProjectionTool.ToPoints(projection, scale, centerX, centerY);
        }
    }
}
