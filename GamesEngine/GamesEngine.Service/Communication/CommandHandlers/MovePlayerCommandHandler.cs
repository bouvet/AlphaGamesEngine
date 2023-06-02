using GamesEngine.Communication.Queries;
using GamesEngine.Patterns.Query;
using GamesEngine.Service.Game.Object;
using GamesEngine.Service.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamesEngine.Patterns.Command;
using GamesEngine.Service.Communication.Commands;
using GamesEngine.Patterns;
using System.Collections;
using System.Numerics;
using GamesEngine.Math;
using GamesEngine.Service.Client;

namespace GamesEngine.Service.Communication.CommandHandlers
{
    public class MovePlayerCommandHandler : ICommandHandler<MovePlayerCommand, ICommandCallback<string>>
    {

        public void Handle(MovePlayerCommand command, ICommandCallback<string> callback)
        {
            IClient client = GameHandler.GetClient(command.ConnectionId);
            IGameObject gameObject = GameHandler.GetGame(command.ConnectionId).FindGameObject(client.PlayerGameObject.Id);

            if (gameObject != null)
            {
                IVector updatePosition = new Math.Vector(0f, 0f, 0f);

                switch (command.KeyboardEvent)
                {
                    case "ArrowRight":
                    case "d":
                        updatePosition = new Math.Vector(10.0f, 0.0f, 0.0f);
                        break;
                    case "ArrowLeft":
                    case "a":
                        updatePosition = new Math.Vector(-10.0f, 0.0f, 0.0f);
                        break;
                    case "ArrowUp":
                    case "w":
                        updatePosition = new Math.Vector(0.0f, -10.0f, 0.0f);
                        break;
                    case "ArrowDown":
                    case "s":
                        updatePosition = new Math.Vector(0.0f, 10.0f, 0.0f);
                        break;
                }

                gameObject.WorldMatrix.SetPosition(gameObject.WorldMatrix.GetPosition().Add(updatePosition));
                
                if (updatePosition.GetX() > 0 || updatePosition.GetY() > 0 || updatePosition.GetZ() > 0)
                {
                    callback.OnSuccess("success");
                }
                else
                {
                    callback.OnFailure();
                }
            }
            else
            {
                callback.OnFailure();
            }
        }
    }
}
