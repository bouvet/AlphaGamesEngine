using System.Numerics;
using GamesEngine.Service.Game;
using GamesEngine.Service.Game.Object;
using Vector = GamesEngine.Math.Vector;

namespace GamesEngine.Service.Camera
{
    public interface IOctoTree<T>
    {
        List<T> FindVisibleObjects(ICamera camera);
        List<T> FindNearestNeighbours(IGameObject gameObject, float radius);
    }

    public class OctoTree<T> : IOctoTree<T>
    {
        public OctoTreeNode<T> Root { get; set; } = new OctoTreeNode<T>();

        public List<T> FindVisibleObjects(ICamera camera)
        {
            throw new NotImplementedException();
        }

        public List<T> FindNearestNeighbours(IGameObject gameObject, float radius)
        {
            throw new NotImplementedException();
        }
    }

    public class OctoTreeNode<T>
    {
        public Vector Position { get; set; }
        public T Data { get; set; }
        public OctoTreeNode<T>[] Children { get; set; } = new OctoTreeNode<T>[8];

        public OctoTreeNode() { }
        public OctoTreeNode(T value)
        {
            Data = value;
        }
    }
}