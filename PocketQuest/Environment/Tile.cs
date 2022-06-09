using PocketQuest.Graphics.LinearAlgebraR3;
using PocketQuest.Graphics.Shapes;

namespace PocketQuest.Environment
{
    public class Tile
    {
        private const int BACK = 0;
        private const int FRONT = 1;

        private readonly List<Triangle> mesh;
        private PointR3 gridLocation;

        /*  NOTE: This constructor assumes vectors are orthogonal 
         *  and are all on the same plane! The arg corner[] has
         *  indices labeled below with the point gridLocation at 
         *  corner 0. Rendering order is from back to front.
         * 
         *  0       1   0--> x      back:       front:
         *  |-------|   |           0--1            1
         *  |   /   |   V           | /           / |
         *  |-------|   y           2            2--3
         *  2       3                                           */
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

        public List<Triangle> GetMesh() { return mesh; }
    }
}
