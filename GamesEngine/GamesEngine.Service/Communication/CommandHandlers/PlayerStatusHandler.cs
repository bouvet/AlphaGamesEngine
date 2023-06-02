using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;
using GamesEngine.Service.Communication.Commands;

namespace GamesEngine.Service.Communication.CommandHandlers;

public class PlayerStatusHandler : ICommandHandler<PlayerStatusCommand, ICommandCallback<string>>
{
    public void Handle(PlayerStatusCommand command, ICommandCallback<string> callback)
    {
        if (command.IsJoin)
        {
            GameHandler.Game.OnConnect(command.ConnectionId);
        }
        else
        {
            GameHandler.Game.OnDisconnect(GameHandler.Game.Clients.Find(e => e.ConnectionId == command.ConnectionId));
        }
    }
}