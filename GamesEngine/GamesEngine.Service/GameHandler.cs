using GamesEngine.Communication;
using GamesEngine.Math;
using GamesEngine.Patterns;
using GamesEngine.Service.Client;
using GamesEngine.Service.Communication;
using GamesEngine.Service.Game;
using GamesEngine.Service.Game.Object.StaticGameObjects;

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
        timer = new Timer(Update, null, 0, 50);

        var size = 14;
        for (var x = 0; x < size; x++)
        {
            for (var y = 0; y < size; y++)
            {
                if(x == 0 || x == size - 1 || y == 0 || y == size - 1)
                {
                    BoxGameObject wallGameObject = new BoxGameObject();
                    wallGameObject.WorldMatrix.SetPosition(new Vector(x - (size/ 2), y - (size / 2), 0));
                    Game.AddGameObject(wallGameObject);
                }
            }
        }
    }

    private static void Update(Object o)
    {
        Game.GameLoop.Update();
    }
}