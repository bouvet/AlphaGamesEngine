using GamesEngine.Patterns;

public delegate void MessageCallback(string senderId, IMessage message);

public interface ICommunicationStrategy
{
    MessageCallback OnMessage { get; }
    void SendToClient(string targetId, IMessage message);
    void SendToAllClients(IMessage message);
}