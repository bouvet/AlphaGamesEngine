using GamesEngine.Patterns.Command;
using GamesEngine.Service.Communication.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Service.Communication.Commands
{
    public interface IShootCommand : ICommand
    {
        int GameObjectId { get; }
    }

    public class CreateBulletCommand : IShootCommand
    {
        public string Type { get; private set; }
        public string? ConnectionId { get; set; }
        public int GameObjectId { get; private set; }

        public CreateBulletCommand(int gameObjectId)
        { 
            Type = "Shoot";
            GameObjectId = gameObjectId;
        }
    }
}

