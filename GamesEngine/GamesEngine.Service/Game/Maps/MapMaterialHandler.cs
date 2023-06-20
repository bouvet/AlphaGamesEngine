using GamesEngine.Service.Game.Object;
using Newtonsoft.Json;

namespace GamesEngine.Service.Game.Maps;

public interface IMapMaterialHandler
{
    public IGameObject GetGameObject(IMapObject mapObject);
}

public class MapMaterialHandler : IMapMaterialHandler
{
    private List<IMapMaterial> MapMaterials = new();

    public MapMaterialHandler()
    {
        var files = Directory.GetFiles("/GameData/Materials", ".*.json");
        foreach (var file in files)
        {
            var material = JsonConvert.DeserializeObject<MapMaterial>(File.ReadAllText(file));
            if (material != null) MapMaterials.Add(material);
        }
    }

    public IGameObject GetGameObject(IMapObject mapObject)
    {
        IGameObject gameObject = null;

        if (mapObject.Type == "Custom")
        {
            mapObject.Properties.TryGetValue("Material", out var materialName);
            var material = MapMaterials.FirstOrDefault(x => x.Name == materialName);

            if (material != null)
            {
                gameObject = mapObject.Static
                    ? new CustomStaticGameObject(material)
                    : new CustomDynamicGameObject(material);
            }
        }

        return gameObject;
    }
}