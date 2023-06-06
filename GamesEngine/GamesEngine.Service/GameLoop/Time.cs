using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Service.GameLoop
{
    public interface ITime
    {
        long GetTime();
    }
    public class Time : ITime
    {
        private readonly long time;
        public Time()
        {
            time = DateTime.Now.Millisecond;
        }

        public long GetTime()
        {
            return time;
        }
    }
}
