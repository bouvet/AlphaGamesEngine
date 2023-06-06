using GamesEngine.Math;
using GamesEngine.Service.GameLoop;

namespace GamesEngine.Service.Game.Object
{
    public abstract class DynamicGameObject : GameObject, IDynamicGameObject
    {
        public IVector Motion { get; set; }
        public abstract void Update(IInterval deltaTime, ITime time);
        public abstract void UpdateMovement(IInterval deltaTime, ITime time);
    }

    public interface IDynamicGameObject : IGameObject
    {
        public IVector Motion { get; set; }
        public void Update(IInterval deltaTime, ITime time);
        public void UpdateMovement(IInterval deltaTime, ITime time);

    }

    public interface IStateMachine
    {

    }
}