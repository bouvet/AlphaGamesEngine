using GamesEngine.Patterns.Command;

namespace GamesEngine.Service.Communication.Commands;

public class PlayerStatusCommand : ICommand
{
    public string Type => "PlayerStatus";
    public bool IsJoin { get; }
    public string ConnectionId { get; set; }

    public PlayerStatusCommand(bool isJoin, string connectionId)
    {
        IsJoin = isJoin;
        ConnectionId = connectionId;
    }
}