using GamesEngine.Service.Game;
using GamesEngine.Service.GameLoop;

namespace GamesEngine.Tests.Fakes;

public class MockGameLoop : IGameLoop
{
    private IGame Game { get; }
    private ITime Time { get; }
    public MockGameLoop(IGame game, ITime time)
    {
        Game = game;
        Time = time;
    }

    public void ProcessInput()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        ITime curTime = new MockTime(0);
        IInterval deltaTime = new Interval(curTime, Time);
        Game.SceneGraph.DynamicGameObject.GetValues().ForEach(gameObject =>
        {
            gameObject.Update(deltaTime, curTime);

            if (gameObject.Motion.GetAbsolute() > 0)
            {
                gameObject.UpdateMovement(deltaTime, curTime);
            }
        });
    }

    public void Render()
    {
        throw new NotImplementedException();
    }
}