using GamesEngine.Service.GameLoop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamesEngine.Math;
using GamesEngine.Service.Client;
using Newtonsoft.Json;

namespace GamesEngine.Service.Game.Object
{
    public interface IPlayerGameObject : IDynamicGameObject
    {
        [JsonIgnore]
        public IClient Client { get; }
        public Vector Motion { get; set; }
    }
    public class PlayerGameObject : DynamicGameObject, IPlayerGameObject
    {
        [JsonIgnore]
        public IClient Client { get; }
        public Vector Motion { get; set; }
        public IMatrix WorldMatrix { get; set; } = new Matrix();
        public IMatrix LocalMatrix { get; set; } = new Matrix();
        public IGameObject Parent { get; set; }
        public List<IGameObject> Children { get; set; }

        public PlayerGameObject(IClient client)
        {
            Client = client;
        }

        public override void Collision(IGameObject gameObject)
        {
            throw new NotImplementedException();
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override void Update(IInterval deltaTime, ITime time)
        {
            throw new NotImplementedException();
        }

        public override void UpdateMovement(IInterval deltaTime, ITime time)
        {
            throw new NotImplementedException();
        }
    }
}
