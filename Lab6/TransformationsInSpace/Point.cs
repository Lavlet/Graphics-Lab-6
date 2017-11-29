using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransformationsInSpace
{
    public class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }
        public double W { get; private set; }

        public Point (double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            W = 1;
        }
    }
}
