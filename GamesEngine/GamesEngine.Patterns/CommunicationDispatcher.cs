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
        return Types(typeof(IQueryHandler<,>));
    }

    public List<Type> CommandHandlers()
    {
        return Types(typeof(ICommandHandler<,>));
    }

    private static List<Type> Types(Type checkType)
    {
        var queryHandlers = new List<Type>();
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        foreach (var type in assembly.GetTypes())
        {
            if (type is not { IsAbstract: false, IsInterface: false }) continue;

            if (type.GetInterfaces().Any(iface =>
                    iface.IsGenericType && iface.GetGenericTypeDefinition() == checkType))
                queryHandlers.Add(type);
        }

        return queryHandlers;
    }
}

public class CommunicationDispatcher : ICommunicationDispatcher
{
    public IDispatcherTypes DispatcherTypes { get; set; } = new DispatcherTypes();

    public void ResolveCommand(ICommand command, CommandCallback callback, FailureCallback failureCallback)
    {
        var method = typeof(CommunicationDispatcher).GetMethod(nameof(InvokeCommandHandler),
            BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var type in DispatcherTypes.CommandHandlers())
            if (type.GetInterfaces().Select(iface => iface.GetGenericArguments()).Any(genericArgs =>
                    genericArgs[0] == command.GetType() && !type.IsAbstract))
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
            }
    }

    public void ResolveQuery(IQuery query, QueryCallback callback, FailureCallback failureCallback)
    {
        var method = typeof(CommunicationDispatcher).GetMethod(nameof(InvokeQueryHandler),
            BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var type in DispatcherTypes.QueryHandlers())
            if (type.GetInterfaces().Select(iface => iface.GetGenericArguments()).Any(genericArgs =>
                    genericArgs[0] == query.GetType() &&
                    typeof(IQueryCallback<string>).IsAssignableFrom(genericArgs[1]) && !type.IsAbstract))
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