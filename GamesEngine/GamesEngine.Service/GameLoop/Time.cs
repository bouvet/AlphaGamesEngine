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
    internal class Time : ITime
    {
        public long GetTime()
        {
            return DateTime.Now.Ticks;
        }
    }
}
