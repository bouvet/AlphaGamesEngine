using GamesEngine.Patterns.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Communication.Queries
{
    public interface IFindGameObjectQuery: IQuery
    {
        int GameObjectId { get; }
    }

    public class FindGameObjectQuery : IFindGameObjectQuery
    {
        public string Type { get; private set; }
        public int GameObjectId { get; private set; }

        public FindGameObjectQuery(int gameObjectId)
        {
            Type = "FindGameObject";
            GameObjectId = gameObjectId;
        }
    }
}
