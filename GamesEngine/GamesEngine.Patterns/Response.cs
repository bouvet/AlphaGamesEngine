namespace GamesEngine.Patterns;

public class Response : IMessage
{
    public string Type { get; }
    public string Content { get; }

    public Response(string type, string content)
    {
        Type = type;
        Content = content;
    }
}