using GamesEngine.Patterns.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesEngine.Service.Communication.Commands
{
    public interface IRotateGameObjectCommand : ICommand
    {
        public int MousePositionX { get; set; }
        public int MousePositionY { get; set; }
    }
    public class RotateGameObjectCommand : IRotateGameObjectCommand
    {
        public string Type {get;set;}
        public string? ConnectionId { get; set; }
        public int MousePositionX { get; set; }
        public int MousePositionY { get; set; }
        public RotateGameObjectCommand(int mousePositionX, int mousePositionY)
        {
            Type = "RotateGameObject";
            MousePositionX = mousePositionX;
            MousePositionY = mousePositionY;
        }
    }
}
