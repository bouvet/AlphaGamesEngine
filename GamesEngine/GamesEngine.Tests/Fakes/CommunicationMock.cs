using GamesEngine.Patterns;
using GamesEngine.Service.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Tests.Fakes
{
    internal class CommunicationMock : ICommunication
    {

        public ICommunicationStrategy CommunicationStrategy => throw new NotImplementedException();

        public ICommunicationDispatcher CommunicationDispatcher => throw new NotImplementedException();

        public void OnMessage(IMessage message)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
