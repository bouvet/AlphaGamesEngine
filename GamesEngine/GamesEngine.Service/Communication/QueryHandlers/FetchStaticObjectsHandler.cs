using GamesEngine.Patterns.Query;
using GamesEngine.Service;
using GamesEngine.Service.Game.Object;
using System.Text.Json;

namespace GamesEngine.Communication.Queries.Handlers;

public class FetchStaticObjectsHandler : IQueryHandler<FetchStaticDynamicObjectsQuery, IQueryCallback<string>>
{
    public void Handle(FetchStaticDynamicObjectsQuery query, IQueryCallback<string> callBack)
    {
        List<IStaticGameObject> objects = GameHandler.GetGame(query.ConnectionId).SceneGraph.StaticGameObject.GetValues();
        callBack.OnSuccess(JsonSerializer.Serialize(objects));
    }
}