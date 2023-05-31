using FluentAssertions;
using GamesEngine.Patterns;
using GamesEngine.Patterns.Command;
using GamesEngine.Patterns.Query;
using GamesEngine.Service;
using GamesEngine.Service.Communication;
using GamesEngine.Tests.Fakes;
using static GamesEngine.Service.Communication.Communication;

namespace GamesEngine.Tests;

[TestFixture]
public class CommunicationTests
{
    private ICommunication Communication;
    private ICommunicationStrategy CommunicationStrategy;
    private ICommunicationDispatcher CommunicationDispatcher;

    public CommunicationTests()
    {
        CommunicationStrategy = new CommunicationStrategyMock((mes) => Communication.OnMessage(mes));
        CommunicationDispatcher = new CommunicationDispatcherMock(
            new List<Type> { typeof(MockQueryHandler) },
         new List<Type> { });
        Communication = new CommunicationMock(CommunicationStrategy, CommunicationDispatcher);
    }

    [Test]
    public void ShouldBeAbleToSendMessage()
    {
        // Arrange
        IMessage message = new QueryMock();

        // Act
        Communication.SendMessage(message);

        // Assert
        ((CommunicationMock)Communication).Result.Should().Be("Success");
    }
}