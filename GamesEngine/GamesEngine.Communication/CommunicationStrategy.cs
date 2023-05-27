using GamesEngine.Patterns;

public interface ICommunicationStrategy
{
    void OnSend(IMessage message);
    void OnReceive(IMessage message);
}

public class CommunicationStrategy : ICommunicationStrategy
{
    public void OnReceive(IMessage message)
    {
        throw new NotImplementedException();
    }

    public void OnSend(IMessage message)
    {
        throw new NotImplementedException();
    }
}