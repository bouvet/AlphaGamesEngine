using FluentAssertions;
using GamesEngine.Communication;
using GamesEngine.Patterns;
using GamesEngine.Tests.Fakes;
using Newtonsoft.Json;

namespace GamesEngine.Tests;

[TestFixture]
public class SignalRTests
{
    [Test]
    public void ShouldDeserialize()
    {
        // Arrange
        IMessage mockMessage = new CommandMock();
        var result = "";
        SignalRCommunicationStrategy signalRCommunicationStrategy = new SignalRCommunicationStrategy((user, mes) =>
        {
            result = mockMessage.GetType() == mes.GetType() ? "Success" : "Failure";
        });
        var json = JsonConvert.SerializeObject(mockMessage);

        // Act
        signalRCommunicationStrategy.HandleMessage("user", json);

        // Assert
        result.Should().Be("Success");
    }
}