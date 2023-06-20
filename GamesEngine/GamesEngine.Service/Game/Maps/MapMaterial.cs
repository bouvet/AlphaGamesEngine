using System.Drawing;

namespace GamesEngine.Service.Game.Maps;

public interface IMapMaterial
{
    public string Name { get; set; }
    public MaterialColor? Color { get; set; }
    public string? Type { get; set; }
}

public class MapMaterial : IMapMaterial
{
    public string Name { get; set; }
    public MaterialColor? Color { get; set; }
    public string? Type { get; set; }
}

public interface IMaterialColor
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
    public int A { get; set; }
}

public class MaterialColor : IMaterialColor
{
    public int R { get; set; } = 0;
    public int G { get; set; } = 0;
    public int B { get; set; } = 0;
    public int A { get; set; } = 255;
}