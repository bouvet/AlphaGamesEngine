using GamesEngine.Math;
using GamesEngine.Service.Game.Object;

namespace GamesEngine.Service.Camera
{

    public interface ICamera : IDynamicGameObject
    {
        IVector LookAt { get; }
    }
}