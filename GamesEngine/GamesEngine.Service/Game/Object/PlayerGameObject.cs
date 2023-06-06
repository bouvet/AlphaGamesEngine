using GamesEngine.Service.GameLoop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamesEngine.Math;
using GamesEngine.Service.Client;
using GamesEngine.Service.Game.Bounds;
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
        public string Type => "Player";

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

        public override bool Collision(IGameObject gameObject)
        {
            return GetBounds().Intersects(gameObject.GetBounds());
        }

        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override void Update(IInterval deltaTime, ITime time)
        {
        }


        //TODO Get IBounds given a specific position
        public IBounds GetBounds()
        {
            return new Bounds.Bounds(WorldMatrix, 1,1,1);
        }

        public override void UpdateMovement(IInterval deltaTime, ITime time)
        {
            float multiplier = deltaTime.GetInterval() / 100f;
            IVector curPos = WorldMatrix.GetPosition();
            IVector moved = Motion.Copy().Multiply(new Vector(multiplier, multiplier, multiplier));
            curPos.Add(moved);

            var matrix = new Matrix();
            matrix.SetPosition(curPos);
            Bounds.Bounds bounds = new Bounds.Bounds(matrix, 1, 1, 1);

            foreach (var staticOb in GameHandler.GetGame(Client.ConnectionId).SceneGraph.StaticGameObject.GetValues())
            {
                if (bounds.Intersects(staticOb.GetBounds()))
                {
                    //TODO Instead of stopping movement, cap movement to the limit of the bounding box
                    //WorldMatrix.SetPosition(curPos);
                    return;
                }
            }

            var newMotion = Motion.Copy();
            newMotion.Subtract(moved);
            Motion = newMotion;
            WorldMatrix.SetPosition(curPos);
        }
    }
}
