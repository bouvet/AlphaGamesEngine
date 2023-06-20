using System.Drawing;

namespace GamesEngine.Service.Game.Maps;

public interface IMapMaterial
{
    public string Name { get; set; }
    public Color Color { get; set; }
    public string Type { get; set; }
}

public class MapMaterial : IMapMaterial
{
    public string Name { get; set; }
    public Color Color { get; set; }
    public string Type { get; set; }
}