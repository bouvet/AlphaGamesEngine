using GamesEngine.Service.Game;
using Math;

namespace GamesEngine.Service.Camera;

public interface ICamera : IDynamicGameObject
{
    IVector LookAt { get; }
}