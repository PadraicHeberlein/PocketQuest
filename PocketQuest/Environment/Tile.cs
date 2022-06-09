using PocketQuest.Graphics.LinearAlgebraR3;
using PocketQuest.Graphics.Shapes;

namespace PocketQuest.Environment
{
    public class Tile
    {
        private const int BACK = 0;
        private const int FRONT = 1;

        private List<Triangle> mesh;
        private PointR3 gridLocation;

        /* this constructor assumes vectors are orthogonal 
         * and are all on the same plane!
         * 
         * corner array indices and direction
         * (gridLocation is at corner 0):
         * 
         *  0       1   y
         *  |-------|   ^
         *  |   /   |   |__> x
         *  |-------|
         *  2       3   */
        public Tile(VectorR3[] corners)
        {
            mesh = new List<Triangle>();

            List<VectorR3> backCorners = new List<VectorR3>();
            List<VectorR3> frontCorners = new List<VectorR3>();

            backCorners.Add(corners[0]);
            backCorners.Add(corners[1]);
            backCorners.Add(corners[2]);

            frontCorners.Add(corners[1]);
            frontCorners.Add(corners[2]);
            frontCorners.Add(corners[3]);

            Triangle back = new Triangle(backCorners.ToArray());
            Triangle front = new Triangle(frontCorners.ToArray());

            mesh.Add(back);
            mesh.Add(front);

            gridLocation = corners[0].ToPoint();
        }
    }
}
