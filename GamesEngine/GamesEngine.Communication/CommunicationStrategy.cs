using GamesEngine.Patterns;

public delegate void MessageCallback(IMessage message);

public interface ICommunicationStrategy
{
    MessageCallback OnMessage { get; }
    void SendToClient(string targetId, IMessage message);
    void SendToAllClients(IMessage message);
}