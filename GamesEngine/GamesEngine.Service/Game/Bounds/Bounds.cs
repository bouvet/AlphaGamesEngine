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
            return Contains(bounds.Position.GetPosition().GetX(), bounds.Position.GetPosition().GetY(), bounds.Position.GetPosition().GetZ())
                   || Contains(bounds.Position.GetPosition().GetX() + bounds.Width, bounds.Position.GetPosition().GetY() + bounds.Height, bounds.Position.GetPosition().GetZ() + bounds.Depth)
                   || Contains(bounds.Position.GetPosition().GetX() + bounds.Width, bounds.Position.GetPosition().GetY() + bounds.Height, bounds.Position.GetPosition().GetZ())
                   || Contains(bounds.Position.GetPosition().GetX(), bounds.Position.GetPosition().GetY() + bounds.Height, bounds.Position.GetPosition().GetZ() + bounds.Depth)
                   || Contains(bounds.Position.GetPosition().GetX() + bounds.Width, bounds.Position.GetPosition().GetY(), bounds.Position.GetPosition().GetZ() + bounds.Depth)
                   || Contains(bounds.Position.GetPosition().GetX(), bounds.Position.GetPosition().GetY() + bounds.Height, bounds.Position.GetPosition().GetZ())
                   || Contains(bounds.Position.GetPosition().GetX(), bounds.Position.GetPosition().GetY(), bounds.Position.GetPosition().GetZ() + bounds.Depth)
                   || Contains(bounds.Position.GetPosition().GetX() + bounds.Width, bounds.Position.GetPosition().GetY(), bounds.Position.GetPosition().GetZ());
        }

        public bool Contains(IBounds bounds)
        {
            return Contains(bounds.Position.GetPosition().GetX(), bounds.Position.GetPosition().GetY(), bounds.Position.GetPosition().GetZ())
                && Contains(bounds.Position.GetPosition().GetX() + bounds.Width, bounds.Position.GetPosition().GetY() + bounds.Height, bounds.Position.GetPosition().GetZ() + bounds.Depth)
                && Contains(bounds.Position.GetPosition().GetX() + bounds.Width, bounds.Position.GetPosition().GetY() + bounds.Height, bounds.Position.GetPosition().GetZ())
                && Contains(bounds.Position.GetPosition().GetX(), bounds.Position.GetPosition().GetY() + bounds.Height, bounds.Position.GetPosition().GetZ() + bounds.Depth)
                && Contains(bounds.Position.GetPosition().GetX() + bounds.Width, bounds.Position.GetPosition().GetY(), bounds.Position.GetPosition().GetZ() + bounds.Depth)
                && Contains(bounds.Position.GetPosition().GetX(), bounds.Position.GetPosition().GetY() + bounds.Height, bounds.Position.GetPosition().GetZ())
                && Contains(bounds.Position.GetPosition().GetX(), bounds.Position.GetPosition().GetY(), bounds.Position.GetPosition().GetZ() + bounds.Depth)
                && Contains(bounds.Position.GetPosition().GetX() + bounds.Width, bounds.Position.GetPosition().GetY(), bounds.Position.GetPosition().GetZ());
        }

        public bool Contains(float x, float y, float z)
        {
            return Position.GetPosition().GetX() >= x && Position.GetPosition().GetX() <= x + Width &&
                   Position.GetPosition().GetY() >= y && Position.GetPosition().GetY() <= y + Height &&
                   Position.GetPosition().GetZ() >= z && Position.GetPosition().GetZ() <= z + Depth;
        }
    }
}