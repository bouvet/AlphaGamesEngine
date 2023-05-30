using GamesEngine.Patterns;

public delegate void MessageCallback(IMessage message);

public interface ICommunicationStrategy
{
    MessageCallback OnMessage { get; }
    void SendMessage(IMessage message);
}

public class CommunicationStrategy : ICommunicationStrategy
{
    public MessageCallback OnMessage { get; }

    public void SendMessage(IMessage message)
    {
        throw new NotImplementedException();
    }
}