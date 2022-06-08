using System.Drawing;
using LinearAlgebra;

namespace Graphics3D
{
    public interface IVisibleObject3D : IGameObject3D
    {
        Color GetColor(LineR3 ray);
        void SetColor(Color newColor, LineR3 ray);
        bool IsProjectedOnTo(PlaneR3 screen, LineR3 ray);
    }
}
