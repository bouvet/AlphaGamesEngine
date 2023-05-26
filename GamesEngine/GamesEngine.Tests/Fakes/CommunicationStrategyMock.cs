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
        public void OnReceive(IMessage message)
        {
            throw new NotImplementedException();
        }

        public void OnSend(IMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
