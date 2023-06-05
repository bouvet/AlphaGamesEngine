using System.Numerics;
using Newtonsoft.Json;

namespace GamesEngine.Math
{
    public interface IMatrix
    {
        IVector GetRotation();
        IVector GetScale();
        IVector GetPosition();
        void SetRotation(IVector rotation);
        void SetScale(IVector scale);
        void SetPosition(IVector position);
    }
    public class Matrix : IMatrix
    {
        [JsonProperty]
        Matrix4x4 _matrix = new Matrix4x4();
        public IVector GetRotation()
        {
            return new Vector(_matrix.M11, _matrix.M12, _matrix.M13);
        }
        public IVector GetScale()
        {
            return new Vector(_matrix.M21, _matrix.M22, _matrix.M23);
        }
        public IVector GetPosition()
        {
            return new Vector(_matrix.M41, _matrix.M42, _matrix.M43);
        }
        public void SetRotation(IVector rotation)
        {
            Matrix4x4 tempMatrix = _matrix;
            tempMatrix.M11 = rotation.GetX();
            tempMatrix.M12 = rotation.GetY();
            tempMatrix.M13 = rotation.GetZ();
            _matrix = tempMatrix;
        }
        public void SetScale(IVector scale)
        {
            Matrix4x4 tempMatrix = _matrix;
            tempMatrix.M21 = scale.GetX();
            tempMatrix.M22 = scale.GetY();
            tempMatrix.M23 = scale.GetZ();
            _matrix = tempMatrix;
        }
        public void SetPosition(IVector position)
        {
            Matrix4x4 tempMatrix = _matrix;
            tempMatrix.M41 = position.GetX();
            tempMatrix.M42 = position.GetY();
            tempMatrix.M43 = position.GetZ();
            _matrix = tempMatrix;
        }
    }
}