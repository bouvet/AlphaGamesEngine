using GamesEngine.Patterns.Query;

namespace GamesEngine.Communication.Queries;

public class FetchStaticObjectsQuery : IQuery
{
    public string Type { get; } = "FetchStaticObjects";
    public string? ConnectionId { get; set; }
}