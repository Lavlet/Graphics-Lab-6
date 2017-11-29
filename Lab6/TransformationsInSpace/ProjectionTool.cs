using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransformationsInSpace
{
    class ProjectionTool
    {
        public static double[,] Kavalie(double[,] points, int f, double angle)
        {
            double a = f * Math.Cos(angle);
            double b = f * Math.Sin(angle);

            double[,] transformMatrix = new double[,]
            {
                {1, 0, 0 },
                {0, 1, 0 },
                { a, -b, 0 }
            };

            return MultiplyMatrices(points, transformMatrix);
        }

        public static System.Drawing.Point[] GetPoints(System.Drawing.Point[] points, params int[] indexes)
        {
            var result = new System.Drawing.Point[indexes.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = points[indexes[i]];
            }

            return result;
        }

        //public static double[,] Rotate(House house, double angle)
        //{
        //    var test = new Matrix(house._house);

        //    double[,] transform = new double[,]
        //    {
        //        {Math.Cos(angle), Math.Sin(angle), 0 },
        //        {-Math.Sin(angle), Math.Cos(angle), 0 },
        //        {0, 0, 1 }
        //    };
        
        //    return Matrix.Multiply(test, new Matrix(transform));
        //}

        public static double[,] MultiplyMatrices(double[,] a, double[,] b)
        {
            double[,] result = new double[a.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int j = 0; j < result.GetLength(1); j++)
                {
                    for (int k = 0; k < a.GetLength(1); ++k)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return result;
        }

        public static double[,] ToArray(Point[] points)
        {
            var result = new double[points.Length, 3];

            for (int i = 0; i < result.GetLength(0); i++)
            {
                result[i, 0] = points[i].X;
                result[i, 1] = points[i].Y;
                result[i, 2] = points[i].Z;
            }

            return result;
        }

        public static System.Drawing.Point[] ToPoints(double[,] points, double scale, int centerX, int centerY)
        {
            var result = new System.Drawing.Point[points.GetLength(0)];

            for (int i = 0; i < result.GetLength(0); i++)
            {
                result[i] = new System.Drawing.Point((int)(points[i,0]*scale) + centerX, (int)(points[i, 1] * scale) + centerY);
            }

            return result;
        }
    }
}
