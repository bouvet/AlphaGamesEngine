using System.Collections.Concurrent;
using GamesEngine.Users;
using Newtonsoft.Json;

namespace GamesEngine.Service.Game.Maps;

public interface IMapsHandler
{
    public IMapMaterialHandler MapMaterialHandler { get; set; }
    public List<IGameMap> GetMaps();
    public IGameMap GetMap(string mapName);
    public void AddMap(IGameMap map);
    public void RemoveMap(string mapName);
}

public class MapsHandler : IMapsHandler
{
    private static ConcurrentDictionary<string, IGameMap> GameMaps { get; set; } = new();

    public MapsHandler()
    {
        using var r = new StreamReader("/GameData/GameMaps.json");
        var json = r.ReadToEnd();
        var maps = JsonConvert.DeserializeObject<List<GameMap>>(json);

        foreach (var map in maps)
        {
            AddMap(map);
        }
    }

    public IMapMaterialHandler MapMaterialHandler { get; set; } = new MapMaterialHandler();

    public List<IGameMap> GetMaps()
    {
        return GameMaps.Values.ToList();
    }

    public IGameMap GetMap(string mapName)
    {
        return GameMaps.TryGetValue(mapName, out var map) ? map : throw new Exception($"Map {mapName} not found");
    }

    public void AddMap(IGameMap map)
    {
        GameMaps.TryAdd(map.MapName, map);
    }

    public void RemoveMap(string mapName)
    {
        GameMaps.TryRemove(mapName, out _);
    }
}