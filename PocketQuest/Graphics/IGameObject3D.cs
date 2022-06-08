using LinearAlgebra;

namespace Graphics3D
{
    public interface IGameObject3D
    {
        VectorR3[] GetVertices();
        void SetVertex(int index, VectorR3 vertex);
    }
}
