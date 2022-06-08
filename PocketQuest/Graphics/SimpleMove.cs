using LinearAlgebra;

namespace Graphics3D
{
    public class SimpleMove : IMove
    {
        readonly VectorR3 pointsToWhere;

        public SimpleMove(VectorR3 toWhere) { pointsToWhere = toWhere; }

        public VectorR3 Moves(VectorR3 toMove) { return pointsToWhere.Add(toMove); }
    }
}
