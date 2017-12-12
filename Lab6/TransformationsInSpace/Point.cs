using System;

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

        public static implicit operator double[](Point point)
        {
            var result = new double[3];
            result[0] = point.X;
            result[1] = point.Y;
            result[2] = point.Z;

            return result;
        }

        public static implicit operator Point (double[] array)
        {
            return new Point(array[0], array[1], array[2]);
        }

        public static implicit operator double[,] (Point point)
        {
            var result = new double[1,3];
            result[0, 0] = point.X;
            result[0, 1] = point.Y;
            result[0, 2] = point.Z;

            return result;
        }

        public static implicit operator Point(double[,] array)
        {
            if (array.GetLength(0) == 1)
                return new Point(array[0, 0], array[0, 1], array[0, 2]);
            else
                throw new ArgumentException();            
        }
    }
}
