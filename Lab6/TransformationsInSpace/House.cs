using System.Collections.Generic;

namespace TransformationsInSpace
{
    public enum ProjectionType { Kavalie = 2, Kabine = 1};

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
            new Point(1, 1, 1),

            new Point(0.5, -0.5, 0),
            new Point(0.5, -0.5, 1)
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

        public ProjectionType TypeOfProjection = ProjectionType.Kavalie;

        public System.Drawing.Point[] GetBox(int scale, int centerX, int centerY, out System.Drawing.Point invisible, List<System.Windows.Forms.Label> debug)
        {           
            var points = ProjectionTool.ToArray(_box);
            var projection = ProjectionTool.Projection(points, (double)TypeOfProjection/2, 45);

            int invisibleIndex = ProjectionTool.FindInvisible(points, debug);
            invisibleIndex = 0;

            switch (invisibleIndex)
            {
                case 0:
                    invisibleIndex = 2;
                    break;
                case 1:
                    invisibleIndex = 3;
                    break;
                case 2:
                    invisibleIndex = 0;
                    break;
                case 3:
                    invisibleIndex = 1;
                    break;
                case 4:
                    invisibleIndex = 6;
                    break;
                case 5:
                    invisibleIndex = 7;
                    break;
                case 6:
                    invisibleIndex = 4;
                    break;
                case 7:
                    invisibleIndex = 5;
                    break;
            }

            var result = ProjectionTool.ToPoints(projection, scale, centerX, centerY);
            invisible = result[invisibleIndex];

            return result;
        }
    }
}
