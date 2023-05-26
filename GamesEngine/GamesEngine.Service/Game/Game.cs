using GamesEngine.Service.Client;
using GamesEngine.Service.GameLoop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamesEngine.Service.Game.Graph;

namespace GamesEngine.Service.Game
{
    public interface IGame
    {
        public List<IClient> Clients { get; set; }
        public IGameLoop GameLoop { get; set; }
        public ISceneGraph SceneGraph { get; set; }
    }

    internal class Game : IGame
    {
        public List<IClient> Clients { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IGameLoop GameLoop { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ISceneGraph SceneGraph { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
