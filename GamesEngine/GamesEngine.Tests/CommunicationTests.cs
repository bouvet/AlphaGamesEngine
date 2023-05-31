using GamesEngine.Service;

namespace GamesEngine.Tests;

[TestFixture]
public class CommunicationTests
{
    [Test]
    public void ShouldBeAbleToSendMessage()
    {
        // Arrange
        var communication = new Communication(new CommunicationStrategyMock(), new CommunicationDispatcherMock());
        var message = new MessageMock();

        // Act
        communication.SendMessage(message);

        // Assert
        Assert.Pass();
    }
}