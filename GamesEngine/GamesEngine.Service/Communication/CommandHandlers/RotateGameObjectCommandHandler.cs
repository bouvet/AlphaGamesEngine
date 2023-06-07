using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;
using GamesEngine.Service.Communication.Commands;
using GamesEngine.Service.Game.Object;
using System.Numerics;
using GamesEngine.Math;

namespace GamesEngine.Service.Communication.CommandHandlers
{
    public class RotateGameObjectCommandHandler : ICommandHandler<RotateGameObjectCommand, ICommandCallback<string>>
    {
        public void Handle(RotateGameObjectCommand command, ICommandCallback<string> callback)
        {
            IGameObject gameObject = GameHandler.GetClient(command.ConnectionId).PlayerGameObject;

            var targetX = command.MousePositionX - gameObject.WorldMatrix.GetPosition().GetX();
            var targetY = command.MousePositionY - gameObject.WorldMatrix.GetPosition().GetY();
            double offsetAngle = System.Math.PI / 2;
            double angle = System.Math.Atan2(targetY, targetX) + offsetAngle;
            //Console.WriteLine($"angle {angle}");

            Quaternion rotation = Quaternion.CreateFromYawPitchRoll(0, (float)angle, 0);
            Vector3 rotationVector = new Vector3(gameObject.WorldMatrix.GetRotation().GetX(), gameObject.WorldMatrix.GetRotation().GetY(), gameObject.WorldMatrix.GetRotation().GetZ());
            Vector3 rotatedVector3 = Vector3.Transform(rotationVector, rotation);
            //Console.WriteLine($"Rot vector{rotatedVector3.X} {rotatedVector3.Y} {rotatedVector3.Z}");
            IVector rotatedVector = new Math.Vector(rotatedVector3.X, rotatedVector3.Y, rotatedVector3.Z);
            gameObject.WorldMatrix.SetRotation(rotatedVector);

            if (gameObject != null)
            {
                var rot = CalculateRotation(gameObject.WorldMatrix.GetPosition(), command.MousePositionX, command.MousePositionY);
                IVector rotationVector = new Math.Vector(1, rot, 0);
                gameObject.WorldMatrix.SetRotation(rotationVector);
                callback.OnSuccess("success");
            }
            else
            {
                callback.OnFailure();
            }
        }

        private static float CalculateRotation(IVector vector, float targetX, float targetY)
        {
            Vector3 targetPosition = new Vector3(targetX, targetY, 0);
            Vector3 currentPosition = new Vector3(vector.GetX(), vector.GetY(), 0);
            Vector3 direction = Vector3.Normalize(targetPosition - currentPosition);
            // Calculate the rotation angle in radians
            float rotationAngleRadians = MathF.Atan2(direction.Y, direction.X);

            // Convert the rotation angle to degrees
            float rotationAngleDegrees = rotationAngleRadians * (180.0f / MathF.PI);
            return rotationAngleRadians;
        }
    }
}
