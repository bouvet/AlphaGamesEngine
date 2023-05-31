using GamesEngine.Patterns;
using GamesEngine.Patterns.Command;
using Microsoft.AspNetCore.SignalR;
using GamesEngine.Patterns.Query;

namespace GamesEngine.Communication
{

    public class SignalRCommunicationStrategy : Hub, ICommunicationStrategy
    {
        public MessageCallback OnMessage => throw new NotImplementedException();

        public async void SendToClient(string targetId, IMessage message)
        {
            await Clients.Client(targetId).SendAsync("ClientFunctionName", message);
        }

        public async void SendToAllClients(IMessage message)
        {
            await Clients.All.SendAsync("ClientFunctionName", message);
        }

        public async void SendMessage(IMessage message)
        {
            await Clients.All.SendAsync("ClientFunctionName", message);
        }

        public async void ReceiveMessageFromClient(IMessage message, MessageCallback OnMessage)
        {
            //string Type = message.Type;

            if (message is ICommand)
            {
                //CommunicationDispatcher.ResolveCommand(message);
            }
            else if (message is IQuery)
            {
                //CommunicationDispatcher.ResolveQuery(message);
            }

            //OnMessage(message);
            await Clients.All.SendAsync("ClientFunctionName", message);
        }

    }

    //public class TestSignalR
    //{
    //    public IHubContext<SignalRCommunicationStrategy> HubContext;

    //    public TestSignalR(IHubContext<SignalRCommunicationStrategy> hubContext) 
    //    {
    //        HubContext = hubContext;
    //    }
    //    HubContext.Clients.All.SendAsync("Clientfunctionname");
    //}

}