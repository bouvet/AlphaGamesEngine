namespace GamesEngine.Patterns;

public class Response : IMessage
{
    public string Type { get; }
    public string? ConnectionId { get; set; } = null;
    public string Content { get; }

    public Response() { }

    public Response(string type, string content)
    {
        Type = type;
        Content = content;
    }
}