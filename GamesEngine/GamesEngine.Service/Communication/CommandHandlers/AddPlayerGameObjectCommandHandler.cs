using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;
using GamesEngine.Service.Communication.Commands;
using GamesEngine.Service.Game;
using GamesEngine.Service.Game.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Service.Communication.CommandHandlers
{
    public class AddPlayerGameObjectCommandHandler : ICommandHandler<AddPlayerGameObjectCommand, ICommandCallback<string>>
    {

        public void Handle(AddPlayerGameObjectCommand command, ICommandCallback<string> callback)
        {
            
            throw new NotImplementedException();
        }
    }
}
