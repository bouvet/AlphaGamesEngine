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
        public IVector Motion { get; set; }
    }
    public class PlayerGameObject : DynamicGameObject, IPlayerGameObject
    {
        [JsonIgnore]
        public IClient Client { get; }

        public IVector Motion { get; set; } = new Vector(0,0,0);
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
        }

        public override void UpdateMovement(IInterval deltaTime, ITime time)
        {
            Console.WriteLine(deltaTime.GetInterval());
            float multiplier = deltaTime.GetInterval() / 100f;
            IVector curPos = WorldMatrix.GetPosition();
            IVector moved = Motion.Copy().Multiply(new Vector(multiplier, multiplier, multiplier));
            curPos.Add(moved);
            var newMotion = Motion.Copy();
            newMotion.Subtract(moved);
            Motion = newMotion;
            WorldMatrix.SetPosition(curPos);
        }
    }
}
