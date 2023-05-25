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
        public void Render();
        public void Collision();
    }

    public class GameObject : IGameObject
    {
        public int Id { get; set; }
        public IMatrix WorldMatrix { get; set; }
        public IMatrix LocalMatrix { get; set; }
        public IGameObject Parent { get; set; }
        public List<IGameObject> Children { get; set; }
        public virtual void Collision()
        {
            throw new NotImplementedException();
        }
        public virtual void Render()
        {
            throw new NotImplementedException();
        }
    }
}