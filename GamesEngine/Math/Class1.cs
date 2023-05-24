namespace Math
{
    public interface IVector
    {
        float GetAbsolute();
    }

    public interface IMatrix
    {
        IVector GetPosition();
        IVector GetRotation();
        IVector GetScale();

    }
}