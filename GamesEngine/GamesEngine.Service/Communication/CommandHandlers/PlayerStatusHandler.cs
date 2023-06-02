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
            GameHandler.GetGame(command.ConnectionId).OnConnect(command.ConnectionId);
        }
        else
        {
            GameHandler.GetGame(command.ConnectionId).OnDisconnect(GameHandler.GetClient(command.ConnectionId));
        }
    }
}