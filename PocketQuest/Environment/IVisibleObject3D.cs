using PocketQuest.Graphics.LinearAlgebraR3;

namespace PocketQuest.Environment
{
    public interface IVisibleObject3D : IGameObject3D
    {
        Color GetColor(LineR3 ray);
        void SetColor(Color newColor, LineR3 ray);
        bool IsProjectedOnTo(PlaneR3 screen, LineR3 ray);
    }
}
