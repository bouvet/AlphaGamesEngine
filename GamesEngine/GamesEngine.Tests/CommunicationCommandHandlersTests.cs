using GamesEngine.Patterns.Query;
using GamesEngine.Service.Communication.CommandHandlers;
using GamesEngine.Tests.Fakes.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Tests
{

    [TestFixture]
    public class CommunicationCommandHandlersTests
    {

        // Mocks: player, client object to update position

        MovePlayerCommandMock commandMock = new MovePlayerCommandMock();
        //ICommandCallback<string> callback;

        [Test]
        public void ShouldBeAbleToUpdatePlayerPosition()
        {
            MovePlayerCommandHandler commandHandler = new MovePlayerCommandHandler();
            //commandHandler.Handle(commandMock, callback);
        }

    }
}
