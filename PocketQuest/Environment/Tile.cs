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
            // Create list to instantiate back triangle...
            List<VectorR3> backCorners = new List<VectorR3>();
            backCorners.Add(corners[0]);
            backCorners.Add(corners[1]);
            backCorners.Add(corners[2]);
            // ...create list to instantiate front triangle...
            List<VectorR3> frontCorners = new List<VectorR3>();
            frontCorners.Add(corners[1]);
            frontCorners.Add(corners[2]);
            frontCorners.Add(corners[3]);
            // Instantiate triangles...
            Triangle back = new Triangle(backCorners.ToArray());
            Triangle front = new Triangle(frontCorners.ToArray());
            // ...add to mesh...
            mesh.Add(back);
            mesh.Add(front);
            // ...and get the grid location.
            gridLocation = corners[0].ToPoint();
        }

        public Tile(PointR3[] corners)
        {
            mesh = new List<Triangle>();
            // Create list to instantiate back triangle...
            List<VectorR3> backCorners = new List<VectorR3>();
            backCorners.Add(corners[0].ToVector());
            backCorners.Add(corners[1].ToVector());
            backCorners.Add(corners[2].ToVector());
            // ...create list to instantiate front triangle...
            List<VectorR3> frontCorners = new List<VectorR3>();
            frontCorners.Add(corners[1].ToVector());
            frontCorners.Add(corners[2].ToVector());
            frontCorners.Add(corners[3].ToVector());
            // Instantiate triangles...
            Triangle back = new Triangle(backCorners.ToArray());
            Triangle front = new Triangle(frontCorners.ToArray());
            // ...add to mesh...
            mesh.Add(back);
            mesh.Add(front);
            // ...and get the grid location.
            gridLocation = corners[0];
        }

        public List<Triangle> GetMesh() { return mesh; }
    }
}
