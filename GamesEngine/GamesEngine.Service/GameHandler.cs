using GamesEngine.Service.Game;

namespace GamesEngine.Service;

public class GameHandler
{
    public static IGame Game { get; set; } = new Game.Game();
}