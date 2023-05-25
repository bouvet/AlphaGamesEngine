namespace Math
{
    public interface IVector
    {
        float GetAbsolute();
        float GetX();
        float GetY();
        float GetZ();
    }

    public interface IMatrix
    {
        IVector GetPosition();
        IVector GetRotation();
        IVector GetScale();
    }
}