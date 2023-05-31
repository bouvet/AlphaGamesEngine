using GamesEngine.Patterns;
using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;

namespace GamesEngine.Service.Communication
{

    public interface ICommunication
    {
        ICommunicationStrategy CommunicationStrategy { get; }
        ICommunicationDispatcher CommunicationDispatcher { get; }

        void SendToClient(string targetId, IMessage message);
        void SendToAllClients(IMessage message);
        void OnMessage(string senderId, IMessage message);
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

        public void OnMessage(string senderId, IMessage message)
        {
            switch (message)
            {
                case IQuery query:
                    CommunicationDispatcher.ResolveQuery(query,
                    (response) =>
                    {
                        SendToClient(senderId, new Response(query.Type, response));
                    },
                    () =>
                    {
                        //TODO Failure
                    });
                    break;

                case ICommand command:
                    CommunicationDispatcher.ResolveCommand(command,
                    (response) =>
                    {
                        SendToClient(senderId, new Response(command.Type, response));
                    },
                    () =>
                    {
                        //TODO Failure
                    });
                    break;
            }
        }

        public void SendToClient(string targetId, IMessage message)
        {
            CommunicationStrategy.SendToClient(targetId, message);
        }

        public void SendToAllClients(IMessage message)
        {
            CommunicationStrategy.SendToAllClients(message);
        }
    }
}

