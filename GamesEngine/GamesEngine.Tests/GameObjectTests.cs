using FluentAssertions;
using GamesEngine.Service.Game;
using GamesEngine.Service.Game.Object;

namespace GamesEngine.Tests;

[TestFixture]
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
        GameObject gameObjectNotNull = new GameObject();
        gameObjectNotNull.Should().NotBeNull();
    }
}