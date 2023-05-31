using GamesEngine.Patterns;
using GamesEngine.Service.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;

namespace GamesEngine.Tests.Fakes
{
    internal class CommunicationMock : ICommunication
    {
        public string Result = null;

        public ICommunicationStrategy CommunicationStrategy { get; private set; }
        public ICommunicationDispatcher CommunicationDispatcher { get; private set; }

        public CommunicationMock(ICommunicationStrategy communicationStrategy,
            ICommunicationDispatcher communicationDispatcher)
        {
            CommunicationStrategy = communicationStrategy;
            CommunicationDispatcher = communicationDispatcher;
        }

        public void OnMessage(IMessage message)
        {
            switch (message)
            {
                case IQuery query:
                    CommunicationDispatcher.ResolveQuery(query, response =>
                    {
                        Result = response;
                    }, () => {});
                    break;
                case ICommand command:
                    CommunicationDispatcher.ResolveCommand(command, response =>
                    {
                        Result = response;
                    }, () => {});
                    break;
            }
        }

        public void SendMessage(IMessage message)
        {
            CommunicationStrategy.SendMessage(message);
        }
    }
}
