using System;
using System.Drawing;
using LinearAlgebra;

namespace Graphics3D
{
    public class Polygon : PlaneR3, IVisibleObject3D
    {
        // named constants for the points that define a plane
        private const int P0 = PointR3.P0;
        private const int P1 = PointR3.P1;
        private const int P2 = PointR3.P2;
        // private class members
        private VectorR3[] vertices;
        private int n;
        private Color color;
        // default constructor for child classes
        public Polygon(int numberOfSides) : base()
        {
            vertices = new VectorR3[numberOfSides];
            n = vertices.Length;
            for (int node = 0; node < n; node++)
                vertices[node] = new VectorR3();
        }
        // vector array constructor
        // NOTE: vector orientation must follow the
        // right hand rule for the normal vector to
        // follow convention, i.e., starting from
        // vertices[0] they must point in a CCW direction
        public Polygon(VectorR3[] corners) : base(corners[P0].ToPoint(), corners[P1].ToPoint(), corners[P2].ToPoint())
        {
            n = corners.Length;
            vertices = new VectorR3[n];
            for (int node = 0; node < n; node++)
                vertices[node] = new VectorR3(corners[node]);
        }
        // point array constructor
        public Polygon(PointR3[] corners) : base(corners[P0], corners[P1], corners[P2])
        {
            n = corners.Length;
            vertices = new VectorR3[n];
            for (int node = 0; node < n; node++)
            {
                vertices[node] = new VectorR3(corners[node]);
                // System.out.println(vertices[node]);
            }
        }
        // copy constructor
        public Polygon(Polygon p) : base(p.vertices[0].ToPoint(), p.vertices[1].ToPoint(), p.vertices[2].ToPoint())
        {

            vertices = new VectorR3[NumSides()];
            for (int node = 0; node < n; node++)
                vertices[node] = new VectorR3(p.vertices[node]);
        }

        // method to check if this is hit by ray coming from user's screen
        public bool IsProjectedOnTo(PlaneR3 screen, LineR3 ray)
        {
            PointR3 eye = ray.GetOrigin();
            if (!ray.Intersects(this))
                return false;
            else
            {
                PointR3 ip = new PointR3();
                try
                {
                    ip = ray.FindIntersectionWith(this);
                    if (!(eye.IsOnSameSideOf(screen, ip)) && Contains(ip))
                    {
                        // System.out.println(ip);
                        return true;
                    }
                }
                catch (Exception e) { Console.WriteLine(e); }
            }
            return false;
        }
        // gets the values of the vertices
        public VectorR3[] GetVertices()
        {
            // NOTE: this smells... will have to try to find another way of doing
            // this. LineR3 and PlaneR3 form LinearAlgebra both rely on the 
            // points from the PlanR3 class to do much needed math, but these
            // points also need to get moved with the IVisibleObject3D...
            // ...the SetVertex method below suffers from the same thing!
            int n = vertices.Length;
            int numPlanePoints = 3;  // 3 points define a plane in R3
            VectorR3[] planeVertices = new VectorR3[numPlanePoints];
            planeVertices[0] = GetPoint(PointR3.P0).ToVector();
            planeVertices[1] = GetPoint(PointR3.P1).ToVector();
            planeVertices[2] = GetPoint(PointR3.P2).ToVector();
            // the plane vertices get tacked on at the end
            VectorR3[] allVertices = new VectorR3[n + numPlanePoints];
            vertices.CopyTo(allVertices, 0);
            planeVertices.CopyTo(allVertices, n);

            return allVertices;
        }
        // sets the values of the vertices
        public void SetVertex(int index, VectorR3 vertex)
        {
            int n = vertices.Length;
            if (index < n)
                vertices[index] = vertex;
            else
            {
                int planeIndex = index - n;
                SetPoint(planeIndex, vertex.ToPoint());
            }
        }
        // get method for the number of vertices this has
        public int NumSides()
        {
            return n;
        }
        // get and set methods for the color of this polygon
        public Color GetColor(LineR3 ray)
        {
            return color;
        }
        public void SetColor(Color newColor, LineR3 ray)
        {
            color = newColor;
        }
        // method for determining if a point is contained in this polygon
        public bool Contains(PointR3 p)
        {
            // is p isn't even on this plane we can
            // return false immediately
            if (!p.IsOnPlane(this))
                return false;
            // the vector along a side conforming to the right hand rule
            // when crossed with normal vector of this polygon, will
            // produce a vector on this plane pointing outward
            PlaneR3[] sidePlanes = new PlaneR3[n];
            VectorR3 normal = GetNormal();
            int numPlainsIsUnder = 0;
            // loop over all sides and construct a plane perpendicular
            // to each side with outwards facing normal
            for (int side = 0; side < n; side++)
            {
                VectorR3 sideDir;
                if (side == n - 1)
                    sideDir = vertices[0].Sub(vertices[side]);
                else
                    sideDir = vertices[side + 1].Sub(vertices[side]);
                sidePlanes[side] = new PlaneR3(sideDir, normal, vertices[side].ToPoint());
            }
            // check how many side-planes p is under, i.e,  
            // on the side oposite from normal facing
            for (int side = 0; side < n; side++)
            {
                if (sidePlanes[side].F(p) < 0)
                    numPlainsIsUnder++;
            }
            // if its under all of them, the this polygon contains p
            return numPlainsIsUnder == n ? true : false;
        }
    }
}
