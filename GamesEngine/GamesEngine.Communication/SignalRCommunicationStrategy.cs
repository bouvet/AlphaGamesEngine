using GamesEngine.Patterns;
using GamesEngine.Patterns.Command;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GamesEngine.Communication
{

    public class SignalRCommunicationStartegy : Hub, ICommunicationStrategy
    {
        public SignalRCommunicationStartegy()
        {
            
        }
        public MessageCallback OnMessage { get; }
        public void SendMessage(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}