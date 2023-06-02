using GamesEngine.Patterns.Query;
using GamesEngine.Service;
using GamesEngine.Service.Game.Object;
using System.Text.Json;

namespace GamesEngine.Communication.Queries.Handlers;

public class FetchDynamicObjectsHandler : IQueryHandler<FetchDynamicObjectsQuery, IQueryCallback<string>>
{
    public void Handle(FetchDynamicObjectsQuery query, IQueryCallback<string> callBack)
    {
        List<IDynamicGameObject> objects = GameHandler.GetGame(query.ConnectionId).SceneGraph.DynamicGameObject.GetValues();
        callBack.OnSuccess(JsonSerializer.Serialize(objects));
    }
}