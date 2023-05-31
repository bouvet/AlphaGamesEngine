using GamesEngine.Patterns;
using GamesEngine.Patterns.Command;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GamesEngine.Communication
{

    public class SignalRCommunicationStartegy : Hub<IGameClient>, ICommunicationStrategy
    {
        // SendToServer(), SendToClient() (update interface?), must be able to communicate outside hub, and must be generic (not signalR specific)

        MessageCallback ICommunicationStrategy.OnMessage => throw new NotImplementedException();

        // Game loop calls this method? Sends game tree
        public void SendMessage(IMessage message)
        {
            throw new NotImplementedException();
        }

        public override async Task OnConnectedAsync()
        {

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {

        }

    }

    public interface IGameClient
    {
    }
}