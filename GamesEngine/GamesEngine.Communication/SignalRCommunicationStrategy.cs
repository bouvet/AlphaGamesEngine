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
        public void OnMessage(IMessage message)
        {
            throw new System.NotImplementedException();
        }

        public void OnSend(IMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}