namespace GamesEngine.Service.Game
{
    public interface IDynamicGameObject
    {
        float deltaTime { get; set; }
        void Update(float deltaTime);
    }
}