using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Service.GameLoop
{
    public interface IInterval
    {
        public long GetInterval();
    }
    public class Interval : IInterval
    {
        private readonly long interval;
        public Interval(ITime startTime, ITime endTime)
        {
            interval = endTime.GetTime() - startTime.GetTime();
        }

        public long GetInterval()
        {
            return interval;
        }
    }
}
