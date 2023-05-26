using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Patterns
{
    public interface ICommunicationDispatcher
    {
        void ResolveCommand(ICommand command);
        void ResolveQuery(IQuery query);
    }
}
