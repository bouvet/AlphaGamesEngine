using GamesEngine.Patterns.Command;

namespace GamesEngine.Tests.Fakes;

public class CommandMock : ICommand
{
    public string Type { get; }
}