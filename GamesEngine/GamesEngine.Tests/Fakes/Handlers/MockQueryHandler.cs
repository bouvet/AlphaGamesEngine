using GamesEngine.Patterns.Query;

namespace GamesEngine.Tests.Fakes;

public class MockQueryHandler : IQueryHandler<QueryMock, IQueryCallback<string>>
{
    public void Handle(QueryMock query, IQueryCallback<string> callBack)
    {
        callBack.OnSuccess("Success");
    }
}