using System.Numerics;

namespace GamesEngine.Math
{
    public interface IVector
    {
        float GetAbsolute();
        float GetX();
        float GetY();
        float GetZ();
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
    }
}