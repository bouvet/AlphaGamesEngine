using System.Numerics;
namespace GamesEngine.Service
{
    public interface IGameObject
    {
        int Id { get; set; }
        Matrix4x4 WorldMatrix { get; set; }
        Matrix4x4 LocalMatrix { get; set; }
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
        public Matrix4x4 WorldMatrix { get; set; }
        public Matrix4x4 LocalMatrix { get; set; }
        public IGameObject Parent { get; set; }
        public List<IGameObject> Children { get; set; }

        public abstract void Collision();

        public (float x, float y, float z) GetPosition()
        {
            return (WorldMatrix.M41, WorldMatrix.M42, WorldMatrix.M43);
        }

        public abstract void Render();
        

        public void SetPosition(float x, float y, float z)
        {
            Matrix4x4 Tempmatrix4X4 = this.WorldMatrix;
            Tempmatrix4X4.M41 = x;
            Tempmatrix4X4.M42 = y;
            Tempmatrix4X4.M43 = z;
            this.WorldMatrix = Tempmatrix4X4;

        }
    }
}