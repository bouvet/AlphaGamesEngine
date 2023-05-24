namespace GamesEngine.Service.Camera
{
    public interface IQuadTree
    {
        List<IGameObject> FindVisableObjects(ICamera camera);
        List<IGameObject> FindNearestNeighbours(IGameObject gameObject, float radius);
    }
}