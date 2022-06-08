using System.Drawing;
using Graphics3D;
using LinearAlgebra;
using System;

namespace Environment
{
    public class Tree : IVisibleObject3D
    {
        Graphics3D.Rectangle trunk;
        Triangle leaves;
        SimpleMove placement;
        Mover placer;

        public Tree(VectorR3 position)
        {
            trunk = MakeTrunk();
            leaves = MakeLeaves();
            placement = new SimpleMove(position);
            placer = new Mover(placement);
            placer.ActsOn(this);
        }

        public bool IsProjectedOnTo(PlaneR3 screen, LineR3 ray)
        {
            if (trunk.IsProjectedOnTo(screen, ray))
                return true;
            if (leaves.IsProjectedOnTo(screen, ray))
                return true;

            return false;
        }

        public Color GetColor(LineR3 ray)
        {
            Color color = Color.Aqua;
            if (ray.Intersects(leaves))
            {
                PointR3 p = ray.FindIntersectionWith(leaves);
                if (leaves.Contains(p))
                    color = leaves.GetColor(ray);
            }
            else if (ray.Intersects(trunk))
            {
                PointR3 p = ray.FindIntersectionWith(trunk);
                if (trunk.Contains(p))
                    color = trunk.GetColor(ray);
            }

            return color;
        }

        public void SetColor(Color newColor, LineR3 ray) 
        {
            if (ray.Intersects(leaves))
                leaves.SetColor(newColor, ray);
            else if (ray.Intersects(trunk))
                trunk.SetColor(newColor, ray);
        }

        public VectorR3[] GetVertices()
        {
            VectorR3[] leavesV = leaves.GetVertices();
            VectorR3[] trunkV = trunk.GetVertices();
            VectorR3[] vertices = new VectorR3[leavesV.Length + trunkV.Length];
            leavesV.CopyTo(vertices, 0);
            trunkV.CopyTo(vertices, leavesV.Length);

            return vertices;
        }

        public void SetVertex(int index, VectorR3 vertex)
        {
            int leavesLength = leaves.GetVertices().Length;
            int trunkLength = trunk.GetVertices().Length;
            if (index >= 0 && index < leavesLength)
                leaves.SetVertex(index, vertex);
            else if (index >= leavesLength && index < trunkLength + leavesLength)
                trunk.SetVertex(index - leavesLength, vertex);
        }
        // constructs a rectangle for the trunk
        private Graphics3D.Rectangle MakeTrunk()
        {
            VectorR3 topLeft = new VectorR3(0, 0.1, 0.3);
            VectorR3 topRight = new VectorR3(0, -0.1, 0.3);
            VectorR3 bottomLeft = new VectorR3(0, 0.1, 0);
            VectorR3 bottomRight = new VectorR3(0, -0.1, 0);

            VectorR3[] corners = new VectorR3[Graphics3D.Rectangle.NUM_SIDES];
            corners[0] = topLeft;
            corners[1] = topRight;
            corners[2] = bottomLeft;
            corners[3] = bottomRight;

            Graphics3D.Rectangle trunk = new Graphics3D.Rectangle(corners);
            trunk.SetColor(Color.BurlyWood, new LineR3());

            return trunk;
        }
        // constructs a triangle for the leaves
        private Triangle MakeLeaves()
        {
            VectorR3 top = new VectorR3(0, 0, 3);
            VectorR3 left = new VectorR3(0, 0.5, 0.3);
            VectorR3 right = new VectorR3(0, -0.5, 0.3);

            VectorR3[] corners = new VectorR3[Triangle.NUM_SIDES];
            corners[0] = top;
            corners[1] = left;
            corners[2] = right;

            Triangle leaves = new Triangle(corners);
            leaves.SetColor(Color.DarkGreen, new LineR3());

            return leaves;
        }

        public void Rotate(VectorR3 axis, double angle)
        {
            double sin = Math.Sin(angle);
            double cos = Math.Cos(angle);
            VectorR3 row1 = VectorR3.E1;
            VectorR3 row2 = new VectorR3(0, cos, -sin);
            VectorR3 row3 = new VectorR3(1, sin, cos);
            MatrixR3 rotateAboutXAxis = new MatrixR3(row1, row2, row3, true);

            MRotationInR3 xAxisRotation = new MRotationInR3(rotateAboutXAxis);
            Rotator rotator = new Rotator(xAxisRotation);

            rotator.ActsOn(this);
        }
    }
}
