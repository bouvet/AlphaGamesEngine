using System.Numerics;
using Math;

namespace GamesEngine.Service
{
    public interface IGameObject
    {
        int Id { get; set; }
        IMatrix WorldMatrix { get; set; }
        IMatrix LocalMatrix { get; set; }
        IGameObject Parent { get; set; }
        List<IGameObject> Children { get; set; }
        public (float x, float y, float z) GetPosition();
        public void SetPosition(float x, float y, float z);
        public void Render();
        public void Collision();
    }

    public abstract class GameObject : IGameObject
    {
        public int Id { get; set; }
        public IMatrix WorldMatrix { get; set; }
        public IMatrix LocalMatrix { get; set; }
        public IGameObject Parent { get; set; }
        public List<IGameObject> Children { get; set; }

        public abstract void Collision();

        public (float x, float y, float z) GetPosition()
        {
            return (WorldMatrix.GetPosition().GetX(), WorldMatrix.GetPosition().GetY(), WorldMatrix.GetPosition().GetZ());
        }

        public abstract void Render();


        public void SetPosition(float x, float y, float z)
        {
            /*
            IMatrix Tempmatrix = this.WorldMatrix;
            Tempmatrix.M41 = x;
            Tempmatrix.M42 = y;
            Tempmatrix.M43 = z;
            this.WorldMatrix = Tempmatrix;
            */
        }
    }
}