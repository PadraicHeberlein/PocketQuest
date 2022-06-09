using PocketQuest.Graphics.LinearAlgebraR3;

namespace PocketQuest.Environment
{
    public interface IGameObject3D
    {
        VectorR3[] GetVertices();
        void SetVertex(int index, VectorR3 vertex);
    }
}
