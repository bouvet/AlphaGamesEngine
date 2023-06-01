using GamesEngine.Service.GameLoop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamesEngine.Service.Client;

namespace GamesEngine.Service.Game.Object
{
    public interface IPlayerGameObject : IDynamicGameObject
    {
        public IClient Client { get; }
    }
    public class PlayerGameObject : DynamicGameObject, IPlayerGameObject
    {
        public IClient Client { get; }

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
