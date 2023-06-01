using GamesEngine.Math;
using GamesEngine.Service.Game.Bounds;
using GamesEngine.Service.Game.Object;
using GamesEngine.Service.GameLoop;

namespace GamesEngine.Tests.Fakes.GameObjects;

public class MockDynamicObject : IDynamicGameObject
{
    public int Id { get; set; }
    public IMatrix WorldMatrix { get; set; }
    public IMatrix LocalMatrix { get; set; }
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

    public void Update(IInterval deltaTime, ITime time)
    {
        throw new NotImplementedException();
    }

    public void UpdateMovement(IInterval deltaTime, ITime time)
    {
        throw new NotImplementedException();
    }
}