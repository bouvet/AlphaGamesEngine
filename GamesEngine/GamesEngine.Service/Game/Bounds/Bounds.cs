using GamesEngine.Math;

namespace GamesEngine.Service.Game.Bounds
{

    public interface IBounds
    {
        IMatrix Position { get; }
        public float Width { get; }
        public float Height { get; }
        public float Depth { get; }
        public bool Intersects(IBounds bounds);
        public bool Contains(IBounds bounds);
        public bool Contains(float x, float y, float z);
    }

    public class Bounds : IBounds
    {
        public IMatrix Position { get; }
        public float Width { get; }
        public float Height { get; }
        public float Depth { get; }

        public Bounds(IMatrix position, float width, float height, float depth)
        {
            Position = position;
            Width = width;
            Height = height;
            Depth = depth;
        }

        public bool Intersects(IBounds bounds)
        {
            return false;
        }

        public bool Contains(IBounds bounds)
        {
            return false;
        }

        public bool Contains(float x, float y, float z)
        {
            return false;
        }
    }
}