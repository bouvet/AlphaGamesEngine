using GamesEngine.Patterns;
using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Tests.Fakes
{
    internal class CommunicationDispatcherMock : ICommunicationDispatcher
    {
        public void ResolveCommand(ICommand command)
        {
            throw new NotImplementedException();
        }

        public void ResolveQuery(IQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
