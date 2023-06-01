using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;

namespace GamesEngine.Tests.Fakes;

public class DynamicCommandHandler : ICommandHandler<DynamicCommandMock, ICommandCallback<string>>
{
    public void Handle(DynamicCommandMock command, ICommandCallback<string> callback)
    {
        if (command.Success)
        {
            callback.OnSuccess("Success");
        }
        else
        {
            callback.OnFailure();
        }
    }
}