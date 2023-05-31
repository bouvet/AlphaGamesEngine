using GamesEngine.Patterns;
using GamesEngine.Service.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Tests.Fakes
{
    public class CommunicationStrategyMock : ICommunicationStrategy
    {
        public MessageCallback OnMessage { get; }

        public CommunicationStrategyMock(MessageCallback onMessage)
        {
            OnMessage = onMessage;
        }

        public void SendMessage(IMessage message)
        {
            OnMessage(message);
        }
    }
}
