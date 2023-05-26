using GamesEngine.Patterns.Command;

namespace GamesEngine.Service.Communication
{
    public class SignalRCommunicationStartegy : ICommunication
    {
        public ICommunicationStrategy CommunicationStrategy => throw new NotImplementedException();

        public ICommandDispatcher CommandDispatcher => throw new NotImplementedException();

        ICommandDispatcher ICommunication.CommandDispatcher => throw new NotImplementedException();

        public void OnMessage(IQuery query)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(ICommand command)
        {
            throw new NotImplementedException();
        }
    }
}