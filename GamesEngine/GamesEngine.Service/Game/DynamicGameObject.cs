using GamesEngine.Service.GameLoop;

namespace GamesEngine.Service.Game
{
    public abstract class DynamicGameObject : GameObject, IDynamicGameObject
    {
        public abstract void Update(IInterval deltaTime, ITime time);
        public abstract void UpdateMovement(IInterval deltaTime, ITime time);
    }

    public interface IDynamicGameObject : IGameObject
    {
        public void Update(IInterval deltaTime, ITime time);
        public void UpdateMovement(IInterval deltaTime, ITime time);

    }

    public interface IStateMachine
    {

    }
}