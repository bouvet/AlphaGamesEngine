using System.Numerics;
using g3;

namespace GamesEngine.Math
{
    using Line = Tuple<IVector, IVector>;

    public interface IBounds
    {
        IVector Position { get; }
        IVector Rotation { get; }
        public float Width { get; }
        public float Height { get; }
        public float Depth { get; }
        public bool Intersects(IBounds bounds);
        public IVector GetIntersection(IBounds bounds);
        public bool Contains(IBounds bounds);
        public bool Contains(float x, float y, float z);
    }

    public class OrientedBounds : IBounds
    {
        public IVector Position { get; }
        public IVector Rotation { get; }
        private IVector EndPosition { get; }

        public float Width { get; }
        public float Height { get; }
        public float Depth { get; }

        private Line[] Edges;

        public OrientedBounds(IMatrix position, float width, float height, float depth)
        {
            Position = position.GetPosition();
            Rotation = position.GetRotation();
            Width = width;
            Height = height;
            Depth = depth;

            float rotX = Rotation.GetX();
            float rotY = Rotation.GetY();
            float rotZ = Rotation.GetZ();

            IVector direction = Vector.GetDirectionVector(rotX, rotY);
            EndPosition = new Vector(Position.GetX() + direction.GetX() * Width, Position.GetY() + direction.GetY() * Height, Position.GetZ() + direction.GetZ() * Depth);

            Width = System.Math.Abs((EndPosition - Position).GetX());
            Height = System.Math.Abs((EndPosition - Position).GetY());
            Depth = System.Math.Abs((EndPosition - Position).GetZ());

            Edges = GetEdges();
        }

        private Line[] GetEdges()
        {
            Vector[] vertices = {
                new (Position.GetX(), Position.GetY(), Position.GetZ()),
                new (Position.GetX(), Position.GetY(), EndPosition.GetZ()),
                new (Position.GetX(), EndPosition.GetY(), Position.GetZ()),
                new (Position.GetX(), EndPosition.GetY(), EndPosition.GetZ()),
                new (EndPosition.GetX(), Position.GetY(), Position.GetZ()),
                new (EndPosition.GetX(), Position.GetY(), EndPosition.GetZ()),
                new (EndPosition.GetX(), EndPosition.GetY(), Position.GetZ()),
                new (EndPosition.GetX(), EndPosition.GetY(), EndPosition.GetZ())
            };

            Line[] edges = {
                new (vertices[0], vertices[1]),
                new (vertices[0], vertices[2]),
                new (vertices[0], vertices[4]),
                new (vertices[1], vertices[3]),
                new (vertices[1], vertices[5]),
                new (vertices[2], vertices[3]),
                new (vertices[2], vertices[6]),
                new (vertices[3], vertices[7]),
                new (vertices[4], vertices[5]),
                new (vertices[4], vertices[6]),
                new (vertices[5], vertices[7]),
                new (vertices[6], vertices[7])
            };

            return edges;
        }

        public IVector GetIntersection(IBounds bounds)
        {
            if (bounds is OrientedBounds bs)
            {
                Line[] edges1 = Edges;
                Line[] edges2 = bs.Edges;

                foreach (var edge1 in edges1)
                {
                    foreach (var edge2 in edges2)
                    {
                        if (LineIntersects(edge1, edge2))
                        {
                            return IntersectionPoint(edge1, edge2);
                        }
                    }
                }
            }
            return null;
        }

        public static IVector? IntersectionPoint(Line line1, Line line2)
        {
            return null;
        }

        private bool LineIntersects(Line line1, Line line2)
        {
            IVector v1 = line1.Item2 - line1.Item1;
            IVector v2 = line2.Item2 - line2.Item1;
            IVector v3 = line2.Item1 - line1.Item1;

            IVector cross = IVector.Cross(v1, v2);

            if (cross.GetX() == 0 && cross.GetY() == 0 && cross.GetZ() == 0)
                return false; // Lines are parallel

            float dot = IVector.Dot(v3, cross);
            if (System.Math.Abs(dot) > float.Epsilon)
                return false; // Lines are skew and do not intersect

            IVector cross1 = IVector.Cross(v3, v1);
            IVector cross2 = IVector.Cross(v3, v2);

            float dotCross = IVector.Dot(cross1, cross2);
            if (dotCross < 0)
                return false; // Lines do not intersect

            return true; // Lines do intersect
        }

        public bool Intersects(IBounds bounds)
        {
            if (bounds is OrientedBounds bs)
            {
                Line[] edges1 = Edges;
                Line[] edges2 = bs.Edges;

                foreach (var edge1 in edges1)
                {
                    foreach (var edge2 in edges2)
                    {
                        if (LineIntersects(edge1, edge2))
                        {
                            return true;
                        }
                    }
                }
            }

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

    public class Bounds : IBounds
    {
        public IVector Position { get; }
        public IVector Rotation { get; }
        private IVector EndPosition { get; }

        private AxisAlignedBox3d box;

        public float Width { get; }
        public float Height { get; }
        public float Depth { get; }

        public Bounds(IMatrix position, float width, float height, float depth)
        {
            Position = position.GetPosition();
            Rotation = position.GetRotation();
            Width = width;
            Height = height;
            Depth = depth;

            Vector3 vecPos = new Vector3(Position.GetX(), Position.GetY(), Position.GetZ());  // Box's first corner position
            Quaternion rotation = Quaternion.CreateFromYawPitchRoll(0, (float)System.Math.PI/4, 0);  // Box's rotation
            Vector3 boxSize = new Vector3(Width, Height, Depth);  // Size of the box (width, height, depth)

            // Calculate the vector from the given corner to the opposite corner
            Vector3 oppositeCornerVector = boxSize;

            // Rotate this vector by the box's rotation
            Vector3 rotatedVector = Vector3.Transform(oppositeCornerVector, rotation);

            // Add this rotated vector to the given corner's position to get the position of the opposite corner
            Vector3 oppositeCornerPosition = Vector3.Add(vecPos, rotatedVector);

            box = new AxisAlignedBox3d(new Vector3d(vecPos.X, vecPos.Y, vecPos.Z), new Vector3d(oppositeCornerPosition.X, oppositeCornerPosition.Y, oppositeCornerPosition.Z));
        }

        public IVector GetIntersection(IBounds bounds)
        {
            return null;
        }

        public bool Intersects(IBounds bounds)
        {
            if (bounds is Bounds bs)
            {
                return bs.box.Intersects(box);
            }

            return false;
        }

        public bool Contains(IBounds bounds)
        {
            if (bounds is Bounds bs)
            {
                return bs.box.Contains(box);
            }

            return false;
        }

        public bool Contains(float x, float y, float z)
        {
            return box.Contains(new Vector3d(x,y,z));
        }
    }
}