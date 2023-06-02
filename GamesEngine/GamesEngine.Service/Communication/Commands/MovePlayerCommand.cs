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
        public string KeyboardEvent { get; }

    }
    public class MovePlayerCommand : IMovePlayerCommand
    {
        public string Type { get; private set; } = "MovePlayer";
        public string? ConnectionId { get; set; }
        public string KeyboardEvent { get; private set; }

        public MovePlayerCommand(string keyboardEvent)
        {
            KeyboardEvent = keyboardEvent;
        }
    }
}
