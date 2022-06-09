using PocketQuest.Graphics.LinearAlgebraR3;

namespace PocketQuest.Graphics.Shapes
{
    public class Triangle : Polygon
    {
        // enumeration constants
        public const int SIDE_1 = 0;
        public const int SIDE_2 = 1;
        public const int SIDE_3 = 2;
        public const int NUM_SIDES = 3;
        // default constructor
        public Triangle() : base(NUM_SIDES) { }
        // vector constructor
        public Triangle(VectorR3[] corners) : base(corners) { }
        // point constructor
        public Triangle(PointR3[] corners) : base(corners) { }
        // copy constructor
        public Triangle(Triangle t) : base(t) { }
        // method to check if this traiange contains point p
        // making use of barycentric coordinates
        new public bool Contains(PointR3 p)
        {
            double alpha = CalculateAlpha(p);
            double beta = CalculateBeta(p);
            double gamma = CalculateGamma(p);

            bool alphaIn = 0 < alpha && alpha < 1;
            bool betaIn = 0 < beta && beta < 1;
            bool gammaIn = 0 < gamma && gamma < 1;

            return alphaIn && betaIn && gammaIn;
        }
        // private methods for generating barycentric coordinates
        private double CalculateAlpha(PointR3 p)
        {
            VectorR3[] vertices = GetVertices();
            VectorR3 test = new VectorR3(p);
            VectorR3 b = vertices[PointR3.P1];
            VectorR3 c = vertices[PointR3.P2];
            VectorR3 na = c.Sub(b).Cross(test.Sub(b));
            double magSquared = GetNormal().MagSquared();

            return 1 / magSquared * GetNormal().Dot(na);
        }
        private double CalculateBeta(PointR3 p)
        {
            VectorR3[] vertices = GetVertices();
            VectorR3 test = new VectorR3(p);
            VectorR3 a = vertices[PointR3.P0];
            VectorR3 c = vertices[PointR3.P2];
            VectorR3 nb = a.Sub(c).Cross(test.Sub(c));
            double magSquared = GetNormal().MagSquared();

            return 1 / magSquared * GetNormal().Dot(nb);
        }
        private double CalculateGamma(PointR3 p)
        {
            VectorR3[] vertices = GetVertices();
            VectorR3 test = new VectorR3(p);
            VectorR3 a = vertices[PointR3.P0];
            VectorR3 b = vertices[PointR3.P1];
            VectorR3 nc = b.Sub(a).Cross(test.Sub(a));
            double magSquared = GetNormal().MagSquared();

            return 1 / magSquared * GetNormal().Dot(nc);
        }
    }
}
