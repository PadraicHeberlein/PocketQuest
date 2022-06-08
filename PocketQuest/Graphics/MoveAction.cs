using LinearAlgebra;

namespace Graphics3D
{
    public abstract class MoveAction
    {
        private IMove mover;

        public MoveAction(IMove howToMove) { mover = howToMove; }

        public void SetHowToMove(IMove howToMove) { mover = howToMove; }

        public void ActsOn(IGameObject3D thisThing)
        {
            int size = thisThing.GetVertices().Length;
            VectorR3[] oldState = thisThing.GetVertices();
            for (int vertex = 0; vertex < size; vertex++)
            {
                VectorR3 newVertex;
                newVertex = mover.Moves(oldState[vertex]);
                thisThing.SetVertex(vertex, newVertex);
            }
        }
    }
}
