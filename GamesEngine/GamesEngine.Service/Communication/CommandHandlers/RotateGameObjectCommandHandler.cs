using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;
using GamesEngine.Service.Communication.Commands;
using GamesEngine.Service.Game.Object;
using GamesEngine.Service.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using GamesEngine.Math;

namespace GamesEngine.Service.Communication.CommandHandlers
{
    public interface IRotateGameObjectCommandHandler : ICommandHandler<IRotateGameObjectCommand, ICommandCallback<string>> 
    { 
    }
    public class RotateGameObjectCommandHandler : IRotateGameObjectCommandHandler
    {

        private readonly IGame _game;

        public RotateGameObjectCommandHandler(IGame game)
        {
            _game = game;
        }

        public void Handle(IRotateGameObjectCommand command, ICommandCallback<string> callback)
        {
            IGameObject gameObject = _game.FindGameObject(command.GameObjectId);
            var targetX = command.MousePositionX - gameObject.WorldMatrix.GetPosition().GetX();
            var targetY = command.MousePositionY - gameObject.WorldMatrix.GetPosition().GetY();
            double offsetAngle = System.Math.PI / 2;
            double angle = System.Math.Atan2(targetY, targetX) + offsetAngle;
            Quaternion rotation = Quaternion.CreateFromYawPitchRoll(0, (float) angle, 0);
            IVector rotatedVector = Vector3.Transform(gameObject.WorldMatrix.GetRotation(), rotation);
            gameObject.WorldMatrix.SetRotation();

            if ()
            {
                callback.OnSuccess("success");
            }
            else
            {
                callback.OnFailure();
            }

        }
    }
}
