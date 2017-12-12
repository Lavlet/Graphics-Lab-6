using System;

namespace TransformationsInSpace
{
    public static class GeometricTools
    {
        public static double PlaneDotDistance(Point point, Plane plain)
        {
            return Math.Abs(plain.A * point.X + plain.B * point.Y + plain.C * point.Z + plain.D) /
                Math.Sqrt(Math.Pow(plain.A, 2) + Math.Pow(plain.B, 2) + Math.Pow(plain.C, 2));
        }

        public static double IsVisible(Point camera, Plane plane)
        {
            return DotProduct(camera, plane);
        }

        public static double Sqr(double a)
        {
            return a * a;
        }

        public static double DotProduct(double[,] a, double[,] b)
        {
            return a[0, 0] * b[0, 0] + a[0, 1] * b[0, 1] + a[0, 2] * b[0, 2];
        }

        public static double DotProduct(Point camera, Plane plane)
        {
            return plane.A * camera.X + plane.B * camera.Y + plane.C * camera.Z;
        }

        public static double[][] ToJagged(double[,] source)
        {
            var result = new double[source.GetLength(0)][];

            for (int i = 0; i < source.GetLength(0); i++)
            {
                result[i] = new double[source.GetLength(1)];
                for (int j = 0; j < source.GetLength(1); j++)
                {
                    result[i][j] = source[i, j];
                }
            }

            return result;
        }
    }
}
