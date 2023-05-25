using GamesEngine.Patterns.Command;

namespace GamesEngine.Service.Server;

public interface ICommunication
{
    ICommunicationStrategy CommunicationStrategy { get; }
    ICommandDispatcher CommandDispatcher { get; }

    void SendMessage(ICommand command);
    void OnMessage(IQuery query);
}

public class Communication : ICommunication
{
    public ICommunicationStrategy CommunicationStrategy { get; }
    public ICommandDispatcher CommandDispatcher { get; }

    public Communication(ICommunicationStrategy communicationStrategy, ICommandDispatcher commandDispatcher)
    {
        CommunicationStrategy = communicationStrategy;
        CommandDispatcher = commandDispatcher;
    }

    public void OnMessage(IQuery query)
    {
        throw new NotImplementedException();
    }

    public void SendMessage(ICommand command)
    {
        throw new NotImplementedException();
    }
}

public interface ICommunicationStrategy
{
}

public class CommunicationStrategy : ICommunicationStrategy
{

}

