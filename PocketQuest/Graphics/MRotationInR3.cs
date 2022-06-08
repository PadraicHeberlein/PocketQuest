using LinearAlgebra;

namespace Graphics3D
{
    public class MRotationInR3 : IMove
    {
        readonly MatrixR3 rotMatrix;

        public MRotationInR3(MatrixR3 r) { rotMatrix = r; }

        public VectorR3 Moves(VectorR3 toMove) { return rotMatrix.Xv(toMove); }
    }
}
