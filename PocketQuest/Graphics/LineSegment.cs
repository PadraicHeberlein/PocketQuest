using LinearAlgebra;

namespace Graphics3D
{
    public class LineSegmentR3 : LineR3
    {
        private PointR3 p0, p1;

        public LineSegmentR3() : base() { }


        public LineSegmentR3(PointR3 point0, PointR3 point1) : base()
        {
            p0 = point0;
            p1 = point1;
            VectorR3 vOn = new VectorR3(p0);
            VectorR3 vDir = new VectorR3(p1.Sub(p0));
            SetDirection(vDir);
            SetOnPoint(vOn);
        }

        public PointR3 Get(int whichPoint)
        {
            PointR3 p = new PointR3();
            switch (whichPoint)
            {
                case PointR3.P0:
                    p = p0;
                    break;
                case PointR3.P1:
                    p = p1;
                    break;
            }
            return p;
        }

        public void Set(int whichPoint, PointR3 p)
        {
            switch (whichPoint)
            {
                case PointR3.P0:
                    p0 = p;
                    break;
                case PointR3.P1:
                    p1 = p;
                    break;
            }
        }
    }
}

