using GamesEngine.Patterns.Query;

namespace GamesEngine.Tests.Fakes;

public class QueryMock : IQuery
{
    public string Type { get; } = "QueryMock";
    public string ConnectionId { get; set; }
}