using GamesEngine.Patterns;
using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;

namespace GamesEngine.Service.Communication
{

    public interface ICommunication
    {
        ICommunicationStrategy CommunicationStrategy { get; }
        ICommunicationDispatcher CommunicationDispatcher { get; }

        void SendMessage(IMessage message);
        void OnMessage(IMessage message);
    }

    public class Communication : ICommunication
    {
        public ICommunicationStrategy CommunicationStrategy { get; private set; }
        public ICommunicationDispatcher CommunicationDispatcher { get; private set; }

        public Communication(ICommunicationStrategy communicationStrategy,
                             ICommunicationDispatcher communicationDispatcher)
        {
            CommunicationStrategy = communicationStrategy;
            CommunicationDispatcher = communicationDispatcher;
        }

        public void OnMessage(IMessage message)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}

