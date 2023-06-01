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
    private IDispatcherTypes MockDispatcherTypes;

    public CommunicationTests()
    {
        MockDispatcherTypes = new MockDispatcherTypes(
            new List<Type> { typeof(MockQueryHandler) },
            new List<Type> { typeof(MockCommandHandler), typeof(DynamicCommandHandler) });
        CommunicationStrategy = new CommunicationStrategyMock((id, mes) => Communication.OnMessage(id, mes));
        CommunicationDispatcher = new CommunicationDispatcherMock(MockDispatcherTypes);
        Communication = new CommunicationMock(CommunicationStrategy, CommunicationDispatcher);
    }

    [Test]
    public void ShouldFindQueryHandler()
    {
        // Arrange
        IDispatcherTypes dispatcherTypes = new DispatcherTypes();

        // Act
        var queryHandlers = dispatcherTypes.QueryHandlers();

        // Assert
        queryHandlers.Should().NotBeNull();
        queryHandlers.Should().HaveCountGreaterThan(0);
        queryHandlers.Should().Contain(typeof(MockQueryHandler));
    }

    [Test]
    public void ShouldFindCommandHandler()
    {
        // Arrange
        IDispatcherTypes dispatcherTypes = new DispatcherTypes();

        // Act
        var commandHandlers = dispatcherTypes.CommandHandlers();

        // Assert
        commandHandlers.Should().NotBeNull();
        commandHandlers.Should().HaveCountGreaterThan(0);
        commandHandlers.Should().Contain(typeof(MockCommandHandler));
    }

    [Test]
    public void ShouldBeAbleToSendQueryMessage()
    {
        // Arrange
        IMessage message = new QueryMock();

        // Act
        Communication.SendToAllClients(message);

        // Assert
        ((CommunicationMock)Communication).Result.Should().Be("Success");
    }

    [Test]
    public void ShouldBeAbleToSendCommandMessage()
    {
        // Arrange
        IMessage message = new CommandMock();

        // Act
        Communication.SendToAllClients(message);

        // Assert
        ((CommunicationMock)Communication).Result.Should().Be("Success");
    }

    [Test]
    public void ShouldBeAbleToHandleQuery()
    {
        // Arrange
        IMessage message = new QueryMock();
        var result = "";

        // Act
        CommunicationDispatcher.ResolveQuery(message as IQuery,
            (response) =>
            {
                result = "success";
            },
            () =>
            {
                result = "failure";
            });

        // Assert
        result.Should().Be("success");
    }

    [Test]
    public void ShouldBeAbleToHandleCommand()
    {
        // Arrange
        IMessage message = new CommandMock();
        var result = "";

        // Act
        CommunicationDispatcher.ResolveCommand(message as ICommand,
            (response) =>
            {
                result = "success";
            },
            () =>
            {
                result = "failure";
            });

        // Assert
        result.Should().Be("success");
    }

    [Test]
    public void ShouldBeAbleToHandleDynamicCommandFailure()
    {
        // Arrange
        IMessage message = new DynamicCommandMock(false);
        var result = "";

        // Act
        CommunicationDispatcher.ResolveCommand(message as ICommand,
            (response) =>
            {
                result = "success";
            },
            () =>
            {
                result = "failure";
            });

        // Assert
        result.Should().Be("failure");
    }

    [Test]
    public void ShouldBeAbleToHandleDynamicCommandSuccess()
    {
        // Arrange
        IMessage message = new DynamicCommandMock(true);
        var result = "";

        // Act
        CommunicationDispatcher.ResolveCommand(message as ICommand,
            (response) =>
            {
                result = "success";
            },
            () =>
            {
                result = "failure";
            });

        // Assert
        result.Should().Be("success");
    }
}