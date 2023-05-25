using GamesEngine.Service.GameLoop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Service.Game
{
    public interface IPlayerGameObject : IDynamicGameObject
    {

    }
    public class PlayerGameObject : DynamicGameObject, IPlayerGameObject
    {
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
