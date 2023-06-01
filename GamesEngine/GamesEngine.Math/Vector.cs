using System.Numerics;

namespace GamesEngine.Math
{
    public interface IVector
    {
        float GetAbsolute();
        float GetX();
        float GetY();
        float GetZ();

        IVector Add(IVector vector);
        IVector Subtract(IVector vector);
        IVector Multiply(IVector vector);
    }
    public class Vector : IVector
    {
        private Vector3 _vector;
        public Vector(float x, float y, float z)
        {
            _vector = new Vector3(x, y, z);
        }
        public float GetAbsolute()
        {
            return _vector.Length();
        }
        public float GetX()
        {
            return _vector.X;
        }
        public float GetY()
        {
            return _vector.Y;
        }
        public float GetZ()
        {
            return _vector.Z;
        }

        public IVector Add(IVector vector)
        {
            _vector.X += vector.GetX();
            _vector.Y += vector.GetY();
            _vector.Z += vector.GetZ();
            return this;
        }

        public IVector Subtract(IVector vector)
        {
            _vector.X -= vector.GetX();
            _vector.Y -= vector.GetY();
            _vector.Z -= vector.GetZ();
            return this;
        }

        public IVector Multiply(IVector vector)
        {
            _vector.X *= vector.GetX();
            _vector.Y *= vector.GetY();
            _vector.Z *= vector.GetZ();
            return this;
        }
    }
}