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
        public long GetInterval()
        {
            throw new NotImplementedException();
        }
    }
}
