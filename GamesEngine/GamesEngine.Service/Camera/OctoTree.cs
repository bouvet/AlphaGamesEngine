using GamesEngine.Service.Game;
using GamesEngine.Service.Game.Object;

namespace GamesEngine.Service.Camera
{
    public interface IOctoTree
    {
        List<IGameObject> FindVisableObjects(ICamera camera);
        List<IGameObject> FindNearestNeighbours(IGameObject gameObject, float radius);
    }
}