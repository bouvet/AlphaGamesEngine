using GamesEngine.Patterns.Command;

namespace GamesEngine.Tests.Fakes;

public class DynamicCommandMock : ICommand
{
    public bool Success { get; }
    public string Type { get; }

    public DynamicCommandMock(bool success)
    {
        Success = success;
    }
}