﻿using System;

namespace PocketQuest.Graphics.LinearAlgebraR3
{
    public class PointR3
    {
        // Public readonly common points:
        public static readonly PointR3 ORIGIN = new PointR3(0.0,0.0,0.0);
        public static readonly PointR3 E1_POINT = new PointR3(1.0,0.0,0.0);
        public static readonly PointR3 E2_POINT = new PointR3(0.0,1.0,0.0);
        public static readonly PointR3 E3_POINT = new PointR3(0.0,0.0,1.0);
        // Enumeration constants for points defining a plane or vector:
        public const int P0 = 0;
        public const int P1 = 1;
        public const int P2 = 2;
        // Private class members:
        private double x, y, z;
        // Default constructor
        public PointR3()
        {
            x = 0.0;
            y = 0.0;
            z = 0.0;
        }
        // Coordinate constructor:
        public PointR3(double xp, double yp, double zp)
        {
            x = xp;
            y = yp;
            z = zp;
        }
        // Copy constructor:
        public PointR3(PointR3 p)
        {
            x = p.x;
            y = p.y;
            z = p.z;
        }
        // Vector constructor:
        public PointR3(VectorR3 v)
        {
            x = v.Get(CoordinateR3.X);
            y = v.Get(CoordinateR3.Y);
            z = v.Get(CoordinateR3.Z);
        }
        // Method for up-casting to a vector:
        public VectorR3 ToVector()
        {
            return new VectorR3(x, y, z);
        }
        // Multiply point by -1:
        public PointR3 Neg()
        {
            return new PointR3(-x, -y, -z);
        }
        // Add two points:
        public PointR3 Add(PointR3 p)
        {
            return new PointR3(x + p.x, y + p.y, z + p.z);
        }
        // Subtract two points:
        public PointR3 Sub(PointR3 p)
        {
            return Add(p.Neg());
        }
        // Multiply point by a scalar:
        public PointR3 Sx(double scalar)
        {
            return new PointR3(scalar * x, scalar * y, scalar * z);
        }
        // Get method based on enumeration constants:
        public double Get(int coordinate)
        {
            double toReturn = 0;
            switch (coordinate)
            {
                case CoordinateR3.X:
                    toReturn = x;
                    break;
                case CoordinateR3.Y:
                    toReturn = y;
                    break;
                case CoordinateR3.Z:
                    toReturn = z;
                    break;
            }
            return toReturn;
        }
        // Set method based on final constants for index:
        public void Set(int coordinate, double coordinateValue)
        {
            switch (coordinate)
            {
                case CoordinateR3.X:
                    x = coordinateValue;
                    break;
                case CoordinateR3.Y:
                    y = coordinateValue;
                    break;
                case CoordinateR3.Z:
                    z = coordinateValue;
                    break;
            }
        }
        // To check whether a point is on the given plane:
        public bool IsOnPlane(PlaneR3 pl)
        {
            return Math.Abs(pl.F(this)) < Constants.ZERO;
        }
        /* Method to determine if the given point is on 
         * the same side of the given plane as this point.   */
        public bool IsOnSameSideOf(PlaneR3 pl, PointR3 p)
        {
            /* f(x,y,x) = ax + by + cz + d == 0 when 
             * the point (x,y,z) is on the plane, < 0 when 
             * on one side, and > 0 when on the other.       */
            if (IsOnPlane(pl) || p.IsOnPlane(pl))
                return false;
            double thisSide = pl.F(this);
            double thatSide = pl.F(p);
            /* When this side and that side have the same 
             * sign, i.e. they're on the same side, then 
             * there product is always positive.            */
            return thisSide * thatSide > 0;
        }
        // toString() method for printing in tests:
        public override String ToString()
        {
            return " (" + x + ", " + y + ", " + z + ") ";
        }
    }
}
