using PocketQuest.Graphics.LinearAlgebraR3;

namespace PocketQuest.Graphics.Shapes
{
    public class Rectangle : Polygon
    {
        // named constants for enumeration
        public const int SIDE_1 = 0;
        public const int SIDE_2 = 1;
        public const int SIDE_3 = 2;
        public const int SIDE_4 = 3;
        public const int NUM_SIDES = 4;
        // default constructor
        public Rectangle() : base(NUM_SIDES) { }
        // vector constructor
        public Rectangle(VectorR3[] corners) : base(corners) { }
        // point constructor
        public Rectangle(PointR3[] corners) : base(corners) { }
        // copy constructor
        public Rectangle(Rectangle rec) : base(rec) { }
        // toString method for printing values for testing
        public override string ToString()
        {
            VectorR3[] corners = GetVertices();
            string s = "[ ";

            for (int node = 0; node < NUM_SIDES; node++)
                s += corners[node] + " ";

            return s + "]";
        }
    }
}
