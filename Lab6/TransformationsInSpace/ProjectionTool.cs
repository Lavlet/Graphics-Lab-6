using System;
using System.Collections.Generic;
using static TransformationsInSpace.GeometricTools;

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

        private static double[] _previousMoveValues = new double[] { 0, 0, 0 };
        private static int[] previousAngle = new int[] { 0, 0, 0 };
        private static double previousScale = 1.0;

        // f = 1 - Kavalie, f = 0.5 - Kabine
        public static double[,] Projection(double[,] points, double f, double angle)
        {
            angle = angle / 180.0 * Math.PI;

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

        public static void Move(House house, double newValue, int axis)
        {
            var box = ToArray(house.Box);
            double delta = newValue - _previousMoveValues[axis];

            for (int i = 0; i < box.GetLength(0); i++)
            {
                box[i, axis] += delta / 50; //Scale, MustBeVariable
            }

            house.Box = ToPoints(box);
            _previousMoveValues[axis] = newValue;
        }

        public static void Rotate(House house, int newAngle, int axis)
        {
            double deltaAngle = (newAngle - previousAngle[axis]) / 180.0 * Math.PI;
            var box = ToArray(house.Box);

            double[,] transform = new double[1, 1];

            switch (axis)
            {
                case 0:
                    transform = new double[,]
            {
                {1, 0, 0 },
                {0, Math.Cos(deltaAngle), Math.Sin(deltaAngle)},
                {0, -Math.Sin(deltaAngle), Math.Cos(deltaAngle) }
            };
                    break;
                case 1:
                    transform = new double[,]
            {
                {Math.Cos(deltaAngle), 0, Math.Sin(deltaAngle) },
                {0, 1, 0},
                {-Math.Sin(deltaAngle), 0, Math.Cos(deltaAngle) }
            };
                    break;
                case 2:
                    transform = new double[,]
            {
                {Math.Cos(deltaAngle), Math.Sin(deltaAngle), 0 },
                {-Math.Sin(deltaAngle), Math.Cos(deltaAngle), 0 },
                {0, 0, 1 }
            };
                    break;
            }

            previousAngle[axis] = newAngle;
            house.Box = ToPoints(MultiplyMatrices(box, transform));
        }

        public static void Scale(House house, double newScale)
        {
            var box = ToArray(house.Box);
            double deltaScale = newScale / previousScale;

            for (int i = 0; i < box.GetLength(0); i++)
            {
                box[i, 0] *= deltaScale; //MustBeVariable
                box[i, 1] *= deltaScale;
                box[i, 2] *= deltaScale;
            }

            house.Box = ToPoints(box);
            previousScale = newScale;
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



        public static int FindInvisible(double[,] points, int scale, int centerX, int centerY, List<System.Windows.Forms.Label> debug)
        {
            var cameraPoint = new double[,] { { 5, 5, -4 } };
            var projectedCameraPoint = ProjectionTool.Projection(cameraPoint, 1, 45);

            var normal = new Point(1, 1, -1 );
            var projectedNormal = ProjectionTool.Projection(cameraPoint, 1, 45);

            var cameraPlain = new Plane(cameraPoint, normal);//new double[] { 1, 1, -1, -2 };

            var testRes = new List<double>();
            var soTest = ToJagged(points);

            testRes.Add(IsVisible(cameraPoint, new Plane(soTest[0], soTest[2], soTest[6])));
            testRes.Add(IsVisible(cameraPoint, new Plane(soTest[2], soTest[3], soTest[7])));
            testRes.Add(IsVisible(cameraPoint, new Plane(soTest[4], soTest[6], soTest[7])));
            testRes.Add(IsVisible(cameraPoint, new Plane(soTest[0], soTest[1], soTest[5])));
            testRes.Add(IsVisible(cameraPoint, new Plane(soTest[1], soTest[3], soTest[7])));
            testRes.Add(IsVisible(cameraPoint, new Plane(soTest[0], soTest[2], soTest[3])));

            int maxDistanceIndex = 0;
            double maxDistance = 0;
            for (int i = 0; i < points.GetLength(0); i++)
            {
                var currentPoint = new Point(points[i, 0], points[i, 1], points[i, 2]);
                var distance = PlaneDotDistance(currentPoint, cameraPlain);
                debug[i].Text = String.Format("Distance {0}", distance);
                if (distance > maxDistance)
                {
                    maxDistanceIndex = i;
                    maxDistance = distance;
                }
            }

            return maxDistanceIndex;
        }
    }
}
