using System.Numerics;
using GamesEngine.Math;
using GamesEngine.Service.Game.Bounds;

namespace GamesEngine.Service.Game.Object
{
    public interface IGameObject
    {
        int Id { get; set; }
        IMatrix WorldMatrix { get; set; }
        IMatrix LocalMatrix { get; set; }
        IGameObject Parent { get; set; }
        List<IGameObject> Children { get; set; }
        public void Render();
        public void Collision(IGameObject otherGameObject);
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
        public virtual void Collision(IGameObject otherGameObject)
        {
            // Assumption: Both GameObject and other have some form of bounds property.
            // This bounds property could be a simple rectangle for AABB (Axis-Aligned Bounding Box) collision
            // detection, or it could be something more complex for other forms of collision detection.
            // The details of this would depend on how your game is structured.

            var thisBounds = this.GetBounds(); // You would need to implement this method.
            var otherBounds = otherGameObject.GetBounds(); // You would need to implement this method.

            // Check for collision using AABB (Axis-Aligned Bounding Box) collision detection.
            // This is a simple form of collision detection suitable for many 2D games.
            if (thisBounds.Intersects(otherBounds))
            {
                // Collision has occurred. Respond appropriately.
                // The exact details of this would depend on your game. Some possible responses might include:
                // - Destroying one or both of the objects
                // - Changing the direction of one or both of the objects
                // - Triggering some game event
                // - etc.
            }
        }
        public virtual void Render()
        {
            throw new NotImplementedException();
        }
    }
}