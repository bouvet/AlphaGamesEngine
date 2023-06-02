using GamesEngine.Service.Client;
using GamesEngine.Service.GameLoop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamesEngine.Service.Game.Graph;
using GamesEngine.Communication.Queries;
using GamesEngine.Service.Communication;
using GamesEngine.Service.Game.Object;

namespace GamesEngine.Service.Game
{
    public interface IGame
    {
        public List<IClient> Clients { get; set; }
        public IGameLoop GameLoop { get; set; }
        public ISceneGraph SceneGraph { get; set; }

        public void AddGameObject(IGameObject gameObject);
        public void RemoveGameObject(int id);
        public IGameObject FindGameObject(int id);

        public IClient OnConnect(string connectionId);
        public void OnDisconnect(IClient client);
    }

    public class Game : IGame
    {
        public List<IClient> Clients { get; set; } = new List<IClient>();
        public IGameLoop GameLoop { get; set; }
        public ISceneGraph SceneGraph { get; set; } = new SceneGraph();

        public Game()
        {
            GameLoop = new GameLoop.GameLoop(this);
        }

        public IGameObject FindGameObject(int id)
        {
            if (SceneGraph.DynamicGameObject.ContainsKey(id))
            {
                return SceneGraph.DynamicGameObject.Get(id);
            }

            if (SceneGraph.StaticGameObject.ContainsKey(id))
            {
                return SceneGraph.StaticGameObject.Get(id);
            }
            return null;
        }

        public void RemoveGameObject(int id)
        {
            IGameObject gameObject = FindGameObject(id);

            if (gameObject is IDynamicGameObject)
            {
                SceneGraph.DynamicGameObject.Remove(id);
            }
            else if (gameObject is IStaticGameObject)
            {
                SceneGraph.StaticGameObject.Remove(id);
            }
        }

        public void AddGameObject(IGameObject gameObject)
        {
            var highestDynamicID = SceneGraph.DynamicGameObject.GetKeys().Count > 0 ? SceneGraph.DynamicGameObject.GetKeys().Max() : 0;
            var highestStaticID = SceneGraph.StaticGameObject.GetKeys().Count > 0 ? SceneGraph.StaticGameObject.GetKeys().Max() : 0;

            var newId = System.Math.Max(highestDynamicID, highestStaticID) + 1;
            gameObject.Id = newId;

            if (gameObject is IDynamicGameObject dynamicGameObject)
            {
                SceneGraph.DynamicGameObject.Add(newId, dynamicGameObject);
            }
            else if (gameObject is IStaticGameObject staticGameObject)
            {
                SceneGraph.StaticGameObject.Add(newId, staticGameObject);
            }
        }

        public IClient OnConnect(string connectionId)
        {
            IClient client = new Client.Client();
            client.ConnectionId = connectionId;
            Clients.Add(client);

            PlayerGameObject playerGameObject = new PlayerGameObject(client);
            AddGameObject(playerGameObject);

            client.PlayerGameObject = playerGameObject;

            return client;
        }

        public void OnDisconnect(IClient client)
        {
            Clients.Remove(client);
            SceneGraph.DynamicGameObject.Remove(client.PlayerGameObject.Id);
        }
    }
}
