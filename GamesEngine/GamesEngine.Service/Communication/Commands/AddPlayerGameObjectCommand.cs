using GamesEngine.Patterns.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ICommand = GamesEngine.Patterns.Command.ICommand;

namespace GamesEngine.Service.Communication.Commands
{
    public interface IAddGameObjectCommand : ICommand
    {

    }

    public class AddPlayerGameObjectCommand : IAddGameObjectCommand
    {
        public string Type { get; private set; } = "AddGameObject";
        public string? ConnectionId { get; set; }
    }
}
