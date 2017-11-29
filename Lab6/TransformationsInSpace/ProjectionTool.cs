using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransformationsInSpace
{
    class ProjectionTool
    {
        private static Point[] _box = new Point[]
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

        public static void Move(House house, double delta, int axis)
        {
            var box = ToArray(house.Box);
            var initialBox = ToArray(_box);

            for (int i = 0; i < box.GetLength(0); i++)
            {
                box[i, axis] = initialBox[i, axis] + delta / 50; //MustBeVariable
            }

            house.Box = ToPoints(box);
        }

        public static void Rotate(House house, double angle, int axis)
        {
            angle = angle / 180 * Math.PI;
            var initialBox = ToArray(_box);
            double[,] transform = new double[1,1];

            switch (axis)
            {
                case 0:
                    transform = new double[,]
            {
                {1, 0, 0 },
                {0, Math.Cos(angle), Math.Sin(angle)},
                {0, -Math.Sin(angle), Math.Cos(angle) }
            };
                    break;
                case 1:
                    transform = new double[,]
            {
                {Math.Cos(angle), 0, Math.Sin(angle) },
                {0, 1, 0},
                {-Math.Sin(angle), 0, Math.Cos(angle) }
            };
                    break;
                case 2:
                    transform = new double[,]
            {
                {Math.Cos(angle), Math.Sin(angle), 0 },
                {-Math.Sin(angle), Math.Cos(angle), 0 },
                {0, 0, 1 }
            };
                    break;
            }

            house.Box = ToPoints(MultiplyMatrices(initialBox, transform));
        }

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

        public static Point[] ToPoints(double[,] array)
        {
            var result = new Point[array.GetLength(0)];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new Point(array[i, 0], array[i, 1], array[i, 2]);
            }

            return result;
        }

        public static System.Drawing.Point[] ToPoints(double[,] points, double scale, int centerX, int centerY)
        {
            var result = new System.Drawing.Point[points.GetLength(0)];

            for (int i = 0; i < result.GetLength(0); i++)
            {
                result[i] = new System.Drawing.Point((int)(points[i, 0] * scale) + centerX, (int)(points[i, 1] * scale) + centerY);
            }

            return result;
        }
    }
}
