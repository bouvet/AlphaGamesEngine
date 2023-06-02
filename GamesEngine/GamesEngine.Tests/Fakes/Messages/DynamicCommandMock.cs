using GamesEngine.Patterns.Command;

namespace GamesEngine.Tests.Fakes;

public class DynamicCommandMock : ICommand
{
    public bool Success { get; }
    public string Type { get; }
    public string ConnectionId { get; set; }

    public DynamicCommandMock(bool success)
    {
        Success = success;
    }
}