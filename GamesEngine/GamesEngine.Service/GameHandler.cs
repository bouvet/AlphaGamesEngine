using System.Collections.Concurrent;
using GamesEngine.Patterns;
using GamesEngine.Service.Client;
using GamesEngine.Service.Communication;
using GamesEngine.Service.Game;
using GamesEngine.Service.Game.Maps;
using GamesEngine.Users;

namespace GamesEngine.Service;

public static class GameHandler
{
    public static ICommunicationDispatcher CommunicationDispatcher { get; set; }
    public static ICommunicationStrategy CommunicationStrategy { get; set; }
    public static ICommunication Communication { get; set; }

    public static IUserHandler UserHandler { get; set; } = new UserHandler();
    public static IMapsHandler MapsHandler { get; set; } = new MapsHandler();

    private static ConcurrentDictionary<string, int> PlayerGameId = new();
    private static ConcurrentDictionary<int, IGame> Games = new();

    public static void AddGame(int id, IGame game)
    {
        Games.TryAdd(id, game);
    }

    public static void AddPlayerId(string id, int gameId)
    {
        PlayerGameId.TryAdd(id, gameId);
    }

    public static void RemovePlayerId(string id)
    {
        PlayerGameId.TryRemove(id, out _);
    }

    public static IGame GetGame(string id)
    {
        var gameId = PlayerGameId[id];
        return Games[gameId];
    }

    public static void OnPlayerConnect(string connectionId, int? targetGameId)
    {
        if (targetGameId == null)
        {
            targetGameId = Games.Keys.FirstOrDefault();
        }

        if (targetGameId != null)
        {
            var id = (int)targetGameId;
            AddPlayerId(connectionId, id);

            if (!Games.ContainsKey(id))
            {
                AddGame(id, new Game.Game(MapsHandler.GetMaps().ToList()[new Random().Next() * MapsHandler.GetMaps().Count]));
            }
        }

        var game = GetGame(connectionId);
        game.OnConnect(connectionId);
    }

    public static void OnPlayerDisconnect(string connectionId)
    {
        var game = GetGame(connectionId);
        var client = GetClient(connectionId);
        game.OnDisconnect(client);

        if(game.Clients.Count == 0)
        {
            Games.TryRemove(PlayerGameId[connectionId], out _);
        }

        RemovePlayerId(connectionId);
    }

    public static IClient? GetClient(string id)
    {
        return GetGame(id).Clients.Find(e => e.ConnectionId != null && e.ConnectionId == id);
    }

    public static void Start()
    {
        new Timer(Update, null, 0, 50);
    }

    private static void Update(object o)
    {
        foreach (var game in Games.Values)
        {
            game.GameLoop.Update();
        }
    }
}