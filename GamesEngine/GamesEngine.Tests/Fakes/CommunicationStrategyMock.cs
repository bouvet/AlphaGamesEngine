using GamesEngine.Patterns;


namespace GamesEngine.Tests.Fakes
{
    public class CommunicationStrategyMock : ICommunicationStrategy
    {
        public MessageCallback OnMessage { get; }

        public CommunicationStrategyMock(MessageCallback onMessage)
        {
            OnMessage = onMessage;
        }

        public void SendToClient(string targetId, IMessage message)
        {
            OnMessage(targetId, message);
        }

        public void SendToAllClients(IMessage message)
        {
            OnMessage(null, message);
        }
    }
}
