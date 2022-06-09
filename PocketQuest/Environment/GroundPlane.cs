using System;
using PocketQuest.Graphics.LinearAlgebraR3;
using System.Drawing;

namespace PocketQuest.Environment
{
    public class GroundPlane : IVisibleObject3D
    {
        PlaneR3 groundPlane = 
            new PlaneR3(VectorR3.E1, VectorR3.E2, PointR3.ORIGIN);
        Color color = Color.Green;

        public GroundPlane() { }

        public Color GetColor(LineR3 ray) { return color; }
        public void SetVertex(VectorR3 none) { }
        public void SetColor(Color theColor, LineR3 ray) { color = theColor; }
        public VectorR3[] GetVertices()
        {
            VectorR3[] vertices = new VectorR3[1];
            vertices[0] = VectorR3.ORIGIN.ToVector();

            return vertices;
        }
        public void SetVertex(int n, VectorR3 none) { }
        public bool IsProjectedOnTo(PlaneR3 screen, LineR3 ray)
        {
            PointR3 eye = ray.GetOrigin();
            if (!ray.Intersects(groundPlane))
                return false;
            else
            {
                PointR3 ip;
                try
                {
                    ip = ray.FindIntersectionWith(groundPlane);
                    if (!eye.IsOnSameSideOf(screen, ip))
                        return true;
                }
                catch (Exception e) { Console.WriteLine(e); }
            }
            return false;
        }
    }
}
