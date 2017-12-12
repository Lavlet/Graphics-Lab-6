namespace TransformationsInSpace
{
    public class Plane
    {
        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }
        public double D { get; private set; }

        public Plane(Point a, Point b, Point c)
        {
            var temp1 = (b.Y - a.Y) * (c.Z - a.Z) - (c.Y - a.Y) * (b.Z - a.Z);
            var temp2 = (b.X - a.X) * (c.Z - a.Z) - (c.X - a.X) * (b.Z - a.Z);
            var temp3 = (b.X -a.X) * (c.Y-a.Y) - (c.X - a.X) * (b.Y - a.Y)  ;

            A = temp1;
            B = -temp2;
            C = temp3;
            D = -a.X * temp1 + a.Y * temp2 - a.Z * temp3;

            if (D == 0 && A == 0 && B == 0 && C != 0)
                C = 1;
            else if (D == 0 && A == 0 && B != 0 && C == 0)
                B = 1;
            else if (D == 0 && A != 0 && B == 0 && C == 0)
                A = 1;
        }

        public Plane(Point a, double[] normal)
        {
            A = normal[0];
            B = normal[1];
            C = normal[2];
            D = normal[0] * (-a.X) + normal[1] * (-a.Y) + normal[2] * (-a.Z);
        }
    }
}
