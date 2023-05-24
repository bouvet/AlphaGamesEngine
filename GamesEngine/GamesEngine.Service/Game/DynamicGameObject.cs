using GamesEngine.Service.GameLoop;

namespace GamesEngine.Service.Game
{
    public abstract class DynamicGameObject : GameObject, IDynamicGameObject
    {
        public abstract void Update(ITime deltaTime);
        public abstract void UpdateMovement(ITime deltaTime);
    }

    public interface IDynamicGameObject : IGameObject
    {
        public void Update(ITime deltaTime);
        public void UpdateMovement(ITime deltaTime);

    }

}