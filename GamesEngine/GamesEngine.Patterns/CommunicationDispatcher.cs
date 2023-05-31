using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Patterns
{
    public delegate void QueryCallback(string response);
    public delegate void CommandCallback(string response);

    public delegate void FailureCallback();

    public interface ICommunicationDispatcher
    {
        void ResolveCommand(ICommand command, CommandCallback callback, FailureCallback failureCallback);
        void ResolveQuery(IQuery query, QueryCallback callback, FailureCallback failureCallback);
    }

    public class CommunicationDispatcher : ICommunicationDispatcher
    {
        public void ResolveCommand(ICommand command, CommandCallback callback, FailureCallback failureCallback)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            foreach(Type type in assembly.GetTypes())
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))
                {
                    var genericArgs = type.GetGenericArguments();
                    if (genericArgs[0] == command.GetType() && !type.IsAbstract)
                    {
                        ICommandHandler<ICommand, ICommandCallback<string>> qr = (ICommandHandler<ICommand, ICommandCallback<string>>)Activator.CreateInstance(type);
                        qr.Handle(command, new CommandCallback<string>(
                            (response) =>
                            {
                               callback(response);
                            },
                            () =>
                            {
                                failureCallback();
                            }
                        ));
                        break;
                    }
                }
            }        }

        public void ResolveQuery(IQuery query, QueryCallback callback, FailureCallback failureCallback)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            foreach(Type type in assembly.GetTypes())
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))
                {
                    var genericArgs = type.GetGenericArguments();
                    if (genericArgs[0] == query.GetType() && typeof(IQueryCallback<string>).IsAssignableFrom(genericArgs[1]) && !type.IsAbstract)
                    {
                        IQueryHandler<IQuery, IQueryCallback<string>> qr = (IQueryHandler<IQuery, IQueryCallback<string>>)Activator.CreateInstance(type);
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
                        break;
                    }
                }
            }
        }
    }
}
