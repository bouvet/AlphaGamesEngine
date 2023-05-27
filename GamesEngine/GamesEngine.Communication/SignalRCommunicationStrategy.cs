using GamesEngine.Patterns;
using GamesEngine.Patterns.Command;

namespace GamesEngine.Service.Communication
{
    public class SignalRCommunicationStartegy : ICommunication
    {
        public ICommunicationStrategy CommunicationStrategy => throw new NotImplementedException();

        public ICommunicationDispatcher CommunicationDispatcher => throw new NotImplementedException();

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