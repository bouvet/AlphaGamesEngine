using GamesEngine.Patterns;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace GamesEngine.Communication
{

    public class SignalRCommunicationStrategy : Hub, ICommunicationStrategy
    {
        public MessageCallback OnMessage { get; }

        public SignalRCommunicationStrategy(MessageCallback onMessage)
        {
            OnMessage = onMessage;
        }

        public async void SendToClient(string targetId, IMessage message)
        {
            await Clients.Client(targetId).SendAsync("ClientDispatcherFunctionName", message);
        }

        public async void SendToAllClients(IMessage message)
        {
            await Clients.All.SendAsync("ClientDispatcherFunctionName", message);
        }

        public void MessageFromClient(string JsonData)
        {
            DataFromClient? data = JsonSerializer.Deserialize<DataFromClient>(JsonData);

            if (!(data is null))
            {
                string Type = data.Type;
                IMessage message = data.Message; 
                OnMessage(Context.ConnectionId, message);
            }
        }

        public class DataFromClient
        {
            public required string Type { get; set; }
            public required IMessage Message { get; set; }
        }
    }
}