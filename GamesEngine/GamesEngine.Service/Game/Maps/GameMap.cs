namespace GamesEngine.Service.Game.Maps;

public interface IGameMap
{
    public List<IMapObject> Objects { get; set; }
    public string MapName { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

public class GameMap : IGameMap
{
    public List<IMapObject> Objects { get; set; }
    public string MapName { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}