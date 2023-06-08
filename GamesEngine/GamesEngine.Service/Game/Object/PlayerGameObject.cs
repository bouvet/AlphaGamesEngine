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

        public override void Render()
        {
            throw new NotImplementedException();
        }

        public override void Update(IInterval deltaTime, ITime time)
        {
        }

        public IBounds GetBounds()
        {
            return MakeBounds(WorldMatrix);
        }

        private IBounds MakeBounds(IMatrix matrix)
        {
            IMatrix boundsMatrix = matrix.Copy();
            IVector direction = Vector.GetDirectionVector(boundsMatrix);
            //boundsMatrix.SetPosition(boundsMatrix.GetPosition() + (direction * new Vector(boundsMatrix.GetScale().GetX() / 2, 0, 0))); //Shift the bounding box to the center along the X axis
            return new Bounds(WorldMatrix, WorldMatrix.GetScale().GetX(),WorldMatrix.GetScale().GetY(),WorldMatrix.GetScale().GetZ());
        }

        public override void UpdateMovement(IInterval deltaTime, ITime time)
        {
            float multiplier = deltaTime.GetInterval() / 100f;
            IVector moved = Motion * multiplier;
            IVector curPos = WorldMatrix.GetPosition();

            var matrix = new Matrix();
            matrix.SetPosition(curPos);
            matrix.SetRotation(WorldMatrix.GetRotation());
            IBounds bounds = MakeBounds(matrix);


            IGameObject collision = CollisionCheck(GameHandler.GetGame(Client.ConnectionId), this, bounds);

            if (collision != null)
            {
               // return;
            }

            var newMotion = Motion - moved;
            Motion = newMotion;
            WorldMatrix.SetPosition(curPos + moved);
        }

        public void Collision(IGameObject otherGameObject)
        {
            Console.WriteLine("Collision with " + otherGameObject.Type + " " + otherGameObject.Id);
            Console.WriteLine("Player position: " + WorldMatrix.GetPosition());
            Console.WriteLine("Other position: " + otherGameObject.WorldMatrix.GetPosition());
        }
    }
}
