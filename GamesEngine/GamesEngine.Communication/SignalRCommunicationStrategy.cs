using System.Collections.Concurrent;
using GamesEngine.Patterns;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Xml.Linq;

namespace GamesEngine.Communication
{
    public class SignalRHub : Hub
    {
        public static event Action<string, string> OnMessageReceived = delegate { };

        public async Task SendMessage(string message)
        {
            OnMessageReceived?.Invoke(Context.ConnectionId, message);
        }
    }

    public class SignalRCommunicationStrategy : ICommunicationStrategy
    {
        private static readonly string CLIENT_DISPATCHER_FUNCTION_NAME = "ClientDispatcherFunctionName";

        public static IHubContext<SignalRHub> HubContext { get; set; }
        public MessageCallback OnMessage { get; }

        public SignalRCommunicationStrategy(MessageCallback onMessage)
        {
            OnMessage = onMessage;
            SignalRHub.OnMessageReceived += HandleMessage;
        }

        private void HandleMessage( string user, string JsonData)
        {
            Console.WriteLine(JsonData);
            DataFromClient? data = JsonSerializer.Deserialize<DataFromClient>(JsonData);

            if (!(data is null))
            {
                IMessage message = new MyMessage(data);
                OnMessage(user, message);
            }
        }

        public async void SendToClient(string targetId, IMessage message)
        {
            await HubContext.Clients.Client(targetId).SendAsync(CLIENT_DISPATCHER_FUNCTION_NAME, message);
        }

        public async void SendToAllClients(IMessage message)
        {
            await HubContext.Clients.All.SendAsync(CLIENT_DISPATCHER_FUNCTION_NAME, message);
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