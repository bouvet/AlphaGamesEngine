using System.Reflection;
using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;

namespace GamesEngine.Patterns;

public delegate void QueryCallback(string response);

public delegate void CommandCallback(string response);

public delegate void FailureCallback();

public interface ICommunicationDispatcher
{
    void ResolveCommand(ICommand command, CommandCallback callback, FailureCallback failureCallback);
    void ResolveQuery(IQuery query, QueryCallback callback, FailureCallback failureCallback);
}

public interface IDispatcherTypes
{
    List<Type> QueryHandlers();
    List<Type> CommandHandlers();
}

public class DispatcherTypes : IDispatcherTypes
{
    public List<Type> QueryHandlers()
    {
        var queryHandlers = new List<Type>();
        var assembly = Assembly.GetExecutingAssembly();

        top:
        foreach (var type in assembly.GetTypes())
        foreach (var iface in type.GetInterfaces())
        {
            if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))
            {
                queryHandlers.Add(type);
                goto top;
            }
        }

        return queryHandlers;
    }

    public List<Type> CommandHandlers()
    {
        var queryHandlers = new List<Type>();
        var assembly = Assembly.GetExecutingAssembly();

        top:
        foreach (var type in assembly.GetTypes())
        foreach (var iface in type.GetInterfaces())
        {
            if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(ICommandHandler<,>))
            {
                queryHandlers.Add(type);
                goto top;
            }
        }


        return queryHandlers;
    }
}

public class CommunicationDispatcher : ICommunicationDispatcher
{
    public IDispatcherTypes DispatcherTypes { get; set; } = new DispatcherTypes();

    public void ResolveCommand(ICommand command, CommandCallback callback, FailureCallback failureCallback)
    {
        var method = typeof(CommunicationDispatcher).GetMethod(nameof(InvokeCommandHandler), BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var type in DispatcherTypes.CommandHandlers())
        foreach (var iface in type.GetInterfaces())
        {
            var genericArgs = iface.GetGenericArguments();
            if (genericArgs[0] == command.GetType() && !type.IsAbstract)
            {
                var commandType = command.GetType();
                var instance = Activator.CreateInstance(type);
                var genericMethod = method.MakeGenericMethod(commandType, typeof(ICommandCallback<string>));
                genericMethod.Invoke(this, new[]
                {
                    instance, command, new CommandCallback<string>(
                        response => { callback(response); },
                        () => { failureCallback(); }
                    )
                });
                break;
            }
        }
    }

    public void ResolveQuery(IQuery query, QueryCallback callback, FailureCallback failureCallback)
    {
        var method = typeof(CommunicationDispatcher).GetMethod(nameof(InvokeQueryHandler), BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var type in DispatcherTypes.QueryHandlers())
        foreach (var iface in type.GetInterfaces())
        {
            var genericArgs = iface.GetGenericArguments();

            if (genericArgs[0] == query.GetType() &&
                typeof(IQueryCallback<string>).IsAssignableFrom(genericArgs[1]) && !type.IsAbstract)
            {
                var queryType = query.GetType();
                var instance = Activator.CreateInstance(type);
                var genericMethod = method.MakeGenericMethod(queryType, typeof(IQueryCallback<string>));
                genericMethod.Invoke(this, new[]
                {
                    instance, query, new QueryCallback<string>(
                        response => { callback(response); },
                        () => { failureCallback(); }
                    )
                });
                break;
            }
        }
    }

    private void InvokeCommandHandler<TCommand, TCallback>(ICommandHandler<TCommand, TCallback> handler,
        TCommand command, TCallback callback)
        where TCommand : ICommand
        where TCallback : ICommandCallback<string>
    {
        handler.Handle(command, callback);
    }

    private void InvokeQueryHandler<TQuery, TCallback>(IQueryHandler<TQuery, TCallback> handler, TQuery query,
        TCallback callback)
        where TQuery : IQuery
        where TCallback : IQueryCallback<string>
    {
        handler.Handle(query, callback);
    }
}