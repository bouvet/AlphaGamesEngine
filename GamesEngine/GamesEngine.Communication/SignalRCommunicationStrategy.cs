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

        public static IHubContext<SignalRHub>? HubContext { get; set; }
        public MessageCallback OnMessage { get; }

        public SignalRCommunicationStrategy(MessageCallback onMessage)
        {
            OnMessage = onMessage;
            SignalRHub.OnMessageReceived += HandleMessage;
            SignalRHub.OnConnect += OnConnect;
            SignalRHub.OnDisconnect += OnDisconnect;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.IsAbstract || !typeof(IMessage).IsAssignableFrom(type))
                        continue;

                    var instance = (IMessage)Activator.CreateInstance(type);

                    if (instance != null && instance.Type != null)
                    {
                        messageTypeMap[instance.Type] = type;
                    }
                }
            }
        }

        private static readonly Dictionary<string, Type> messageTypeMap = new Dictionary<string, Type>();

        public void HandleMessage(string user, string JsonData)
        {
            IMessage data;
            Dictionary<string, object> json = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonData);
            var type = json.GetValueOrDefault("Type", null).ToString();

            var messageType = messageTypeMap.GetValueOrDefault(type);
            if (messageType != null)
            {
                data = (IMessage)JsonSerializer.Deserialize(JsonData, messageType);
                OnMessage(user, data);
            }
            else
            {
                Console.WriteLine("Unknown message type: " + type);
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
    }
}