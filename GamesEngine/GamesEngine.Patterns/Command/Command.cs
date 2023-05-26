using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Patterns.Command
{

    public interface ICommandDispatcher
    {
        void ResolveCommand(ICommand command);
        void ResolveQuery(IQuery query);
    }

    public interface ICommandHandler
    {
        void Handle(ICommand command);
    }

    public interface IQueryHandler
    {
        void Handle(IQuery command);
    }

    public interface ICommand
    {

    }

    public interface IQuery
    {

    }

}
