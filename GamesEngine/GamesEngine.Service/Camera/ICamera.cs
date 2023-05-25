using GamesEngine.Service.Game;
using GamesEngine.Math;

namespace GamesEngine.Service.Camera;

public interface ICamera : IDynamicGameObject
{
    IVector LookAt { get; }
}