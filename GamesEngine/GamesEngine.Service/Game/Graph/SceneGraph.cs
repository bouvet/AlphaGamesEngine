using System.Collections.Generic;
using GamesEngine.Service.Camera;
using GamesEngine.Service.Game.Object;

namespace GamesEngine.Service.Game.Graph
{
    public interface ISceneGraph
    {
        public SortedDictionary<int, IDynamicGameObject> DynamicGameObject { get; set; }
        public SortedDictionary<int, IStaticGameObject> StaticGameObject { get; set; }
        public IOctoTree OctoTree { get; set; }

    }

    public class SceneGraph : ISceneGraph
    {
        public SortedDictionary<int, IDynamicGameObject> DynamicGameObject { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SortedDictionary<int, IStaticGameObject> StaticGameObject { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IOctoTree OctoTree { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}