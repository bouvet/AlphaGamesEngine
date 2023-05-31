using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamesEngine.Patterns.Query;

namespace GamesEngine.Patterns.Command
{
    public interface ICommandHandler<TCommand, TCallBack>
        where TCommand : ICommand
        where TCallBack : ICommandCallback<string>
    {
        void Handle(TCommand command, TCallBack callback);
    }
}
