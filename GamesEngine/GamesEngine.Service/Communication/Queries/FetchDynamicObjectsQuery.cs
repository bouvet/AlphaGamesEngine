using GamesEngine.Patterns.Query;

namespace GamesEngine.Communication.Queries;

public class FetchDynamicObjectsQuery : IQuery
{
    public string Type { get; } = "FetchDynamicObjects";
    public string? ConnectionId { get; set; }
}