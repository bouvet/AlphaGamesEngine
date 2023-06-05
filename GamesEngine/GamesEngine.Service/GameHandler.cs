using GamesEngine.Communication;
using GamesEngine.Patterns;
using GamesEngine.Service.Client;
using GamesEngine.Service.Communication;
using GamesEngine.Service.Game;

namespace GamesEngine.Service;

public class GameHandler
{
    public static ICommunicationDispatcher CommunicationDispatcher { get; set; }
    public static ICommunicationStrategy CommunicationStrategy { get; set; }
    public static ICommunication Communication { get; set; }
    public static IGame Game { get; set; } = new Game.Game();

    public static IGame GetGame(string id)
    {
        return Game;
    }

    public static IClient GetClient(string id)
    {
        return Game.Clients.Find(e => e.ConnectionId == id);
    }

    private static Timer timer;
    public static void Start()
    {
        timer = new Timer(Update, null, 0, 100);
    }

    private static void Update(Object o)
    {
        Game.GameLoop.Update();
    }
}