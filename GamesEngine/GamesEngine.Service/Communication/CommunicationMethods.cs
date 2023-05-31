using GamesEngine.Patterns;
using GamesEngine.Service.Client;

namespace GamesEngine.Service.Communication;

public static class CommunicationMethods
{
    public static void SendToClient(this Communication communication, IClient client, IMessage message)
    {
        communication.SendToClient(client.ConnectionId, message);
    }
}