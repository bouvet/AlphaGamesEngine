using GamesEngine.Patterns.Query;

namespace GamesEngine.Communication.Queries;

public class FetchStaticDynamicObjectsQuery : IQuery
{
    public string Type { get; } = "FetchStaticDynamicObjects";
    public string? ConnectionId { get; set; }
}