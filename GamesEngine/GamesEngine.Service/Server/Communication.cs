namespace GamesEngine.Service.Server;

public interface ICommunication
{
    ICommunicationStrategy CommunicationStrategy { get; }
    ICommandDispatcher CommandDispatcher { get; }

    void SendMessage(ICommand command);
    void OnMessage(IQuery query);
}

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

public interface ICommunicationStrategy
{

}