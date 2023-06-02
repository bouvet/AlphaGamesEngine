using GamesEngine.Patterns;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Xml.Linq;

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
            Console.WriteLine(JsonData);
            DataFromClient? data = JsonSerializer.Deserialize<DataFromClient>(JsonData);

            if (!(data is null))
            {
                IMessage message = new MyMessage(data);
                OnMessage(Context.ConnectionId, message);
            }
        }

        public class DataFromClient
        {
            public string Type { get; set; }
            public MessageData Message { get; set; }
        }

        public class MessageData
        {
            public int gameObjectId { get; set; }
        }

        public class MyMessage : IMessage
        {
            public string Type { get; }

            public MyMessage(DataFromClient data)
            {
                Type = data.Type;
            }
        }
    }

}