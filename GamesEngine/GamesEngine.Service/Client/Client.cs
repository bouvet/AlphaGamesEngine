using GamesEngine.Service.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Service.Client
{
    public interface IClient
    {
        public ref PlayerGameObject PlayerGameObject { get; }
    }
    public class Client : IClient
    {
        public ref PlayerGameObject PlayerGameObject => throw new NotImplementedException();
    }
}
