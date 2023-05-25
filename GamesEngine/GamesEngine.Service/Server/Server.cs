using GamesEngine.Service.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Service.Server
{
    public interface IServer
    {
        public List<IGame> Games { get; set; }
        public List<IUser> Users { get; set; }
    }
    internal class Server : IServer
    {
        public List<IGame> Games { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<IUser> Users { get; set; }
    }
}
