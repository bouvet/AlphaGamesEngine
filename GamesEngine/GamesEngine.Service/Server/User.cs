namespace GamesEngine.Service.Server
{
    public interface IUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public List<int> Friends { get; set; }

    }
    public class User
    {
    }
}