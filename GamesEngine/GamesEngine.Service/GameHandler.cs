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


        // Create a game world
        var size = 20;
        for (var x = 0; x < size; x++)
        {
            for (var y = 0; y < size; y++)
            {
                if(x == 0 || x == size - 1 || y == 0 || y == size - 1)
                {
                    WallGameObject wallGameObject = new WallGameObject();
                    float height = (x + y) % 2 == 0 ? 1 : 1.5f;

                    wallGameObject.WorldMatrix.SetPosition(new Vector(x - (size/ 2), y - (size / 2), 0));
                    wallGameObject.WorldMatrix.SetScale(new Vector(1, 1, height));
                    Game.AddGameObject(wallGameObject);
                }
                else
                {
                    var rand = new Random();

                    if (rand.NextDouble() < 0.1)
                    {
                        float scale = 0.5f + (float)rand.NextDouble();
                        ObstacleGameObject wallGameObject = new ObstacleGameObject();
                        wallGameObject.WorldMatrix.SetPosition(new Vector(x - (size/ 2), y - (size / 2), 0));
                        wallGameObject.WorldMatrix.SetRotation(new Vector(0, 0, (float)rand.NextDouble() * 360));
                        wallGameObject.WorldMatrix.SetScale(new Vector(scale, scale, scale));
                        Game.AddGameObject(wallGameObject);
                    }
                }

                FloorGameObject floorGameObject = new FloorGameObject();
                floorGameObject.WorldMatrix.SetPosition(new Vector(x - (size / 2), y - (size / 2), -1));
                Game.AddGameObject(floorGameObject);
            }
        }
    }

    private static void Update(Object o)
    {
        Game.GameLoop.Update();
    }
}