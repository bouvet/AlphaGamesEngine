using System.Collections.Concurrent;
using GamesEngine.Patterns;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;
using System.Xml.Linq;
using GamesEngine.Service;
using GamesEngine.Service.Communication.Commands;

namespace GamesEngine.Communication
{
    public class SignalRHub : Hub
    {
        public static event Action<string, string> OnMessageReceived = delegate { };
        public static event Action<string> OnConnect = delegate { };
        public static event Action<string> OnDisconnect = delegate { };

        public async Task SendMessage(string message)
        {
            OnMessageReceived?.Invoke(Context.ConnectionId, message);
        }

        public override Task OnConnectedAsync()
        {
            OnConnect?.Invoke(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            OnDisconnect?.Invoke(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
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
            SignalRHub.OnConnect += OnConnect;
            SignalRHub.OnDisconnect += OnDisconnect;
        }

        private void HandleMessage( string user, string JsonData)
        {
            DataFromClient? data = JsonSerializer.Deserialize<DataFromClient>(JsonData);

            if (!(data is null))
            {
                IMessage message = new MyMessage(data);
                OnMessage(user, message);
            }
        }

        private void OnConnect(string user)
        {
            OnMessage(user, new PlayerStatusCommand(true, user));
        }

        private void OnDisconnect(string user)
        {
            OnMessage(user, new PlayerStatusCommand(false, user));
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
            public string ConnectionId { get; set; }

            public MyMessage(DataFromClient data)
            {
                Type = data.Type;
            }
        }
    }

}