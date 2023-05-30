using GamesEngine.Patterns;

public interface ICommunicationStrategy
{
    delegate void OnMessage(IMessage message);
    void SendMessage(IMessage message);
}

public class CommunicationStrategy : ICommunicationStrategy
{
    public delegate void OnMessage(IMessage message);

    public void SendMessage(IMessage message)
    {
        throw new NotImplementedException();
    }
}