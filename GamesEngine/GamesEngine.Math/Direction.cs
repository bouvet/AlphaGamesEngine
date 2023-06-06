namespace GamesEngine.Math;

public class Direction{
    public static IVector UP => new Vector(0, 1, 0);
    public static IVector DOWN => new Vector(0, -1, 0);
    public static IVector LEFT => new Vector(-1, 0, 0);
    public static IVector RIGHT => new Vector(1, 0, 0);
}