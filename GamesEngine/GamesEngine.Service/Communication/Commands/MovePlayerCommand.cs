using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Service.Communication.Commands
{
    public interface IMovePlayerCommand : ICommand
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }
    public class MovePlayerCommand : IMovePlayerCommand
    {
        public string Type { get; private set; } = "MovePlayer";
        public string? ConnectionId { get; set; }

        public MovePlayerCommand()
        {
        }

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
    }
}
