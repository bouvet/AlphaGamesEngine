using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Patterns
{
    public interface IMessage
    {
        string Type { get; }
        string ConnectionId { get; set;  }
    }
}
