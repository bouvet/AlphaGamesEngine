using GamesEngine.Math;
using GamesEngine.Service.Game.Maps;
using GamesEngine.Service.GameLoop;

namespace GamesEngine.Service.Game.Object;

public interface ICustomGameObject : IGameObject
{
    public IMapMaterial MapMaterial { get; set; }
}

public class CustomStaticGameObject : ICustomGameObject, IStaticGameObject
{
    public string Type => MapMaterial.Type ?? "Custom";

    public int Id { get; set; }
    public IMatrix WorldMatrix { get; set; } = new Matrix();
    public IMatrix LocalMatrix { get; set; } = new Matrix();
    public IGameObject? Parent { get; set; }
    public List<IGameObject> Children { get; set; }
    public void Render() { }

    public void Collision(IGameObject? otherGameObject) { }

    public IBounds GetBounds()
    {
        return new Bounds(WorldMatrix, WorldMatrix.GetScale().GetX(), WorldMatrix.GetScale().GetY(), WorldMatrix.GetScale().GetZ());
    }

    public IMapMaterial MapMaterial { get; set; }

    public CustomStaticGameObject(IMapMaterial mapMaterial)
    {
        MapMaterial = mapMaterial;
    }
}

public class CustomDynamicGameObject : ICustomGameObject, IDynamicGameObject
{
    public string Type => MapMaterial.Type ?? "Custom";
    public IMapMaterial MapMaterial { get; set; }

    public CustomDynamicGameObject(IMapMaterial mapMaterial)
    {
        MapMaterial = mapMaterial;
    }

    public int Id { get; set; }
    public IMatrix WorldMatrix { get; set; } = new Matrix();
    public IMatrix LocalMatrix { get; set; } = new Matrix();
    public IGameObject? Parent { get; set; }
    public List<IGameObject> Children { get; set; }
    public void Render() { }

    public void Collision(IGameObject? otherGameObject) { }

    public IBounds GetBounds()
    {
        return new Bounds(WorldMatrix, WorldMatrix.GetScale().GetX(), WorldMatrix.GetScale().GetY(), WorldMatrix.GetScale().GetZ());
    }

    public IVector Motion { get; set; }
    public void Update(IInterval deltaTime, ITime time){ }

    public void UpdateMovement(IInterval deltaTime, ITime time) { }
}