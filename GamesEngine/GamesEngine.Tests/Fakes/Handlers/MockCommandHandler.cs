using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;

namespace GamesEngine.Tests.Fakes;

public class MockCommandHandler : ICommandHandler<CommandMock, ICommandCallback<string>>
{
    public void Handle(CommandMock command, ICommandCallback<string> callback)
    {
        callback.OnSuccess("Success");
    }
}