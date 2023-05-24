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
    }

    public class GameObject : IGameObject
    {
        public int Id { get; set; }
        public Matrix4x4 WorldMatrix { get; set; }
        public Matrix4x4 LocalMatrix { get; set; }
        public IGameObject Parent { get; set; } = null;
        public List<IGameObject> Children { get; set; } = new List<IGameObject>();

        public GameObject(int id, Matrix4x4 worldMatrix, Matrix4x4 localMatrix)
        {
            Id = id;
            WorldMatrix = worldMatrix;
            LocalMatrix = localMatrix;
        }
    }
}