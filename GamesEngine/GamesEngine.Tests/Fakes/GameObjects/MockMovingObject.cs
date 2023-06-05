using GamesEngine.Math;
using GamesEngine.Service.Game.Bounds;
using GamesEngine.Service.Game.Object;
using GamesEngine.Service.GameLoop;

namespace GamesEngine.Tests.Fakes.GameObjects;

public class MockMovingObject : IDynamicGameObject
{
    public MockMovingObject(Vector motion)
    {
        Motion = motion;
    }

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

    public IVector Motion { get; set; }

    public void Update(IInterval deltaTime, ITime time) { }

    public void UpdateMovement(IInterval deltaTime, ITime time)
    {
        float multiplier = deltaTime.GetInterval() / 1000f; //TODO Replace 1000f with update frequency (1000 = 1s)
        IVector curPos = WorldMatrix.GetPosition();
        curPos.Add(Motion.Multiply(new Vector(multiplier, multiplier, multiplier)));
        WorldMatrix.SetPosition(curPos);
        Motion = new Vector(0, 0, 0);
    }
}