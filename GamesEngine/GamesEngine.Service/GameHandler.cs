using GamesEngine.Service.Client;
using GamesEngine.Service.Game;

namespace GamesEngine.Service;

public class GameHandler
{
    public static IGame Game { get; set; } = new Game.Game();

    public static IClient GetClient(string id)
    {
        return Game.Clients.Find(e => e.ConnectionId == id);
    }
}