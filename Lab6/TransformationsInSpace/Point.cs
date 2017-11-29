using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransformationsInSpace
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public int W { get; private set; }

        public Point (int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            W = 1;
        }
    }
}
