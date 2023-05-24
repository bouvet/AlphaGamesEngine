using System.Collections.Generic;
using GamesEngine.Service.Camera;

namespace GamesEngine.Service.Game
{
    public interface ISceneGraph
    {
        public SortedDictionary<int, IDynamicGameObject> DynamicGameObject { get; set; }
        public SortedDictionary<int, IStaticGameObject> StaticGameObject { get; set; }
        public IQuadTree QuadTree { get; set; }

    }

    public class SceneGraph : ISceneGraph
    {
        public SortedDictionary<int, IDynamicGameObject> DynamicGameObject { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SortedDictionary<int, IStaticGameObject> StaticGameObject { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IQuadTree QuadTree { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}