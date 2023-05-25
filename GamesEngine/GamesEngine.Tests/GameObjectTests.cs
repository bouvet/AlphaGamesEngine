using FluentAssertions;
using GamesEngine.Service;

namespace GamesEngine.Tests;


public class GameObjectTests
{

    // Arrange
    private IGameObject gameObject;

    public GameObjectTests()
    {
        gameObject = new GameObject();
    }

    [Test]
    public void ShouldBeAbleToCreateGameObject()
    {
        gameObject.Should().NotBeNull();
    }
}