using GamesEngine.Math;
using GamesEngine.Service.Game.Bounds;

namespace GamesEngine.Service.Game.Object.StaticGameObjects;

public class BoxGameObject : IStaticGameObject
{
    public string Type => "Wall";
    public int Id { get; set; }
    public IMatrix WorldMatrix { get; set; } = new Matrix();
    public IMatrix LocalMatrix { get; set; } = new Matrix();
    public IGameObject Parent { get; set; }
    public List<IGameObject> Children { get; set; }
    public void Render()
    {
        throw new NotImplementedException();
    }

    public bool Collision(IGameObject otherGameObject)
    {
        var thisBounds = this.GetBounds();
        var otherBounds = otherGameObject.GetBounds();
        if (thisBounds.Intersects(otherBounds))
        {
            return true;
        }
        return false;
    }

    public IBounds GetBounds()
    {
        return new Bounds.Bounds(WorldMatrix, 1, 1, 1);
    }
}