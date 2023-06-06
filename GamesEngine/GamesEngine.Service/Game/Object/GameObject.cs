using System.Numerics;
using GamesEngine.Math;
using GamesEngine.Service.Game.Bounds;

namespace GamesEngine.Service.Game.Object
{
    public interface IGameObject
    {
        string Type => GetType().Name;
        int Id { get; set; }
        IMatrix WorldMatrix { get; set; }
        IMatrix LocalMatrix { get; set; }
        IGameObject Parent { get; set; }
        List<IGameObject> Children { get; set; }
        public void Render();
        public bool Collision(IGameObject otherGameObject);
        public IBounds GetBounds();
    }

    public class GameObject : IGameObject
    {
        public int Id { get; set; }
        public IMatrix WorldMatrix { get; set; }
        public IMatrix LocalMatrix { get; set; }
        public IGameObject Parent { get; set; }
        public List<IGameObject> Children { get; set; }
        public IBounds GetBounds()
        {
            throw new NotImplementedException();
        }
        public virtual bool Collision(IGameObject otherGameObject)
        {
            var thisBounds = this.GetBounds();
            var otherBounds = otherGameObject.GetBounds();
            if (thisBounds.Intersects(otherBounds))
            {
                return true;
            }

            return false;
        }
        public virtual void Render()
        {
            throw new NotImplementedException();
        }
    }
}