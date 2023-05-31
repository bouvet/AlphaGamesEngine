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

        public void SendMessage(IMessage message)
        {
            OnMessage(message);
        }
    }
}
