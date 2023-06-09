using GamesEngine.Service.Game;
using GamesEngine.Service.Game.Object;

namespace GamesEngine.Service.GameLoop;

public interface IGameLoop
{
    public void ProcessInput();
    public void Update();
    public void Render();
}

public class GameLoop : IGameLoop
{
    private ITime lastUpdate = new Time();

    public GameLoop(IGame game)
    {
        Game = game;
    }

    private IGame Game { get; }

    public void ProcessInput()
    {
        throw new NotImplementedException();
    }

    public void Render()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        ITime currentTime = new Time();
        IInterval deltaTime = new Interval(lastUpdate, currentTime);
        Game.SceneGraph.DynamicGameObject.GetValues().ForEach(gameObject =>
        {
            gameObject.Update(deltaTime, currentTime);

            if (gameObject.Motion != null && gameObject.Motion.GetAbsolute() > 0)
            {
                gameObject.UpdateMovement(deltaTime, currentTime);
            }

            GameObject.CollisionCheck(Game, gameObject);
        });
        lastUpdate = new Time();
    }
}