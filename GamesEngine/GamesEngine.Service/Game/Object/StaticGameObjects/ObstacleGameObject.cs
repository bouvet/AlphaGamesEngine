using GamesEngine.Math;
using GamesEngine.Service.GameLoop;

namespace GamesEngine.Service.Game.Object.StaticGameObjects;

public class ObstacleGameObject : IDynamicGameObject
{
    public string Type => "obstacle";
    public int Id { get; set; }
    public IMatrix WorldMatrix { get; set; } = new Matrix();
    public IMatrix LocalMatrix { get; set; } = new Matrix();
    public IGameObject Parent { get; set; }
    public List<IGameObject> Children { get; set; }

    public bool Colliding { get; set; }

    public void Render()
    {
        throw new NotImplementedException();
    }

    public void Collision(IGameObject otherGameObject)
    {
        Colliding = true;
    }

    public IBounds GetBounds()
    {
        return new Bounds(WorldMatrix, WorldMatrix.GetScale().GetX(), WorldMatrix.GetScale().GetY(), WorldMatrix.GetScale().GetZ());
    }

    public long DeltaActiveTime { get; set; }
    public IVector Motion { get; set; }
    public void Update(IInterval deltaTime, ITime time)
    {
        if (Colliding)
        {
            DeltaActiveTime += deltaTime.GetInterval();

            if (DeltaActiveTime > 1000)
            {
                Colliding = false;
                DeltaActiveTime = 0;
            }
        }
    }

    public void UpdateMovement(IInterval deltaTime, ITime time)
    {
        throw new NotImplementedException();
    }
}