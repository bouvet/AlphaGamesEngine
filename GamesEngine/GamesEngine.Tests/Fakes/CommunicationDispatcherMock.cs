using GamesEngine.Patterns;
using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Tests.Fakes
{
    internal class CommunicationDispatcherMock : ICommunicationDispatcher
    {
        private List<Type> QueryHandlers { get; }
        private List<Type> CommandHandlers { get; }

        public CommunicationDispatcherMock(List<Type> queryHandlers, List<Type> commandHandlers)
        {
            QueryHandlers = queryHandlers;
            CommandHandlers = commandHandlers;
        }

        public void ResolveCommand(ICommand command, CommandCallback callback, FailureCallback failureCallback)
        {
            foreach (var handler in CommandHandlers)
            {
                foreach (var type in handler.GetInterfaces())
                {
                    if (type.GetGenericArguments()[0] == command.GetType())
                    {
                        ICommandHandler<ICommand, ICommandCallback<string>> qr =
                            (ICommandHandler<ICommand, ICommandCallback<string>>)Activator.CreateInstance(handler);
                        qr.Handle(command, new CommandCallback<string>(
                            (response) => { callback(response); },
                            () => { failureCallback(); }
                        ));
                        return;
                    }
                }
            }
        }

        public void ResolveQuery(IQuery query, QueryCallback callback, FailureCallback failureCallback)
        {
            foreach (var handler in QueryHandlers)
            {
                foreach (var type in handler.GetInterfaces())
                {
                    if (type.GetGenericArguments()[0] == query.GetType())
                    {
                        IQueryHandler<IQuery, IQueryCallback<string>> qr = (IQueryHandler<IQuery, IQueryCallback<string>>)Activator.CreateInstance(handler);

                        qr.Handle(query, new QueryCallback<string>(
                            (response) =>
                            {
                                callback(response);
                            },
                            () =>
                            {
                                failureCallback();
                            }
                        ));
                        return;
                    }
                }
            }
        }
    }
}
