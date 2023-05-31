using GamesEngine.Patterns;
using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Tests.Fakes
{
    internal class CommunicationDispatcherMock : CommunicationDispatcher
    {
        private List<Type> QueryHandlers { get; }
        private List<Type> CommandHandlers { get; }

        public CommunicationDispatcherMock(List<Type> queryHandlers, List<Type> commandHandlers)
        {
            QueryHandlers = queryHandlers;
            CommandHandlers = commandHandlers;
            DispatcherTypes = new MockDispatcherTypes(QueryHandlers, CommandHandlers);
        }
    }

    internal class MockDispatcherTypes : IDispatcherTypes
    {
        private List<Type> QueryHandlersList { get; }
        private List<Type> CommandHandlersList { get; }

        public MockDispatcherTypes(List<Type> queryHandlers, List<Type> commandHandlers)
        {
            QueryHandlersList = queryHandlers;
            CommandHandlersList = commandHandlers;
        }

        public List<Type> QueryHandlers()
        {
            return QueryHandlersList;
        }

        public List<Type> CommandHandlers()
        {
            return CommandHandlersList;
        }
    }
}
