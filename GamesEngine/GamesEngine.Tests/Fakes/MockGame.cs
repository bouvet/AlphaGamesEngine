using GamesEngine.Service.Client;
using GamesEngine.Service.Game;
using GamesEngine.Service.Game.Graph;
using GamesEngine.Service.Game.Object;
using GamesEngine.Service.GameLoop;

namespace GamesEngine.Tests.Fakes;

public class MockGame : IGame
{
    public List<IClient> Clients { get; set; }
    public IGameLoop GameLoop { get; set; }
    public ISceneGraph SceneGraph { get; set; }
    public void AddGameObject(IGameObject gameObject)
    {
        throw new NotImplementedException();
    }

    public void RemoveGameObject(int id)
    {
        throw new NotImplementedException();
    }

    public IGameObject FindGameObject(int id)
    {
        throw new NotImplementedException();
    }

    public IClient OnConnect(string connectionId)
    {
        throw new NotImplementedException();
    }

    public void OnDisconnect(IClient client)
    {
        throw new NotImplementedException();
    }
}