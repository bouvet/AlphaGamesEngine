using GamesEngine.Service.GameLoop;

namespace GamesEngine.Tests.Fakes;

public class MockTime : ITime
{
    private long time;

    public MockTime(long time)
    {
        this.time = time;
    }

    public long GetTime()
    {
        return time;
    }
}