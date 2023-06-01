using GamesEngine.Patterns.Query;

namespace GamesEngine.Tests.Fakes;

public class QueryMock : IQuery
{
    public string Type { get; } = "QueryMock";
}