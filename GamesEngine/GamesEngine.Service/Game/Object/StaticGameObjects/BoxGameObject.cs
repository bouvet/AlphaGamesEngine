using GamesEngine.Math;
using GamesEngine.Service.Game.Bounds;

namespace GamesEngine.Service.Game.Object.StaticGameObjects;

public class BoxGameObject : IStaticGameObject
{
    public int Id { get; set; }
    public IMatrix WorldMatrix { get; set; } = new Matrix();
    public IMatrix LocalMatrix { get; set; } = new Matrix();
    public IGameObject Parent { get; set; }
    public List<IGameObject> Children { get; set; }
    public void Render()
    {
        throw new NotImplementedException();
    }

    public void Collision(IGameObject otherGameObject)
    {
        throw new NotImplementedException();
    }

    public IBounds GetBounds()
    {
        throw new NotImplementedException();
    }
}