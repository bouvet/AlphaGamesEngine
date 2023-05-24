using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Service.GameLoop
{
    public interface IGameLoop
    {
        public void ProcessInput();
        public void Update();
        public void Render();
    }
    public class GameLoop : IGameLoop
    {
        public void ProcessInput()
        {
            throw new NotImplementedException();
        }

        public void Render()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
