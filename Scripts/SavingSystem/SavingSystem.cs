using Godot;
using Godot.Collections;
using System.Linq;

public partial class SavingSystem : Resource
{

    const string SaveFilePath = "user://SaveFiles/SaveGame.tres";
    [Export] private Array<VillagerResourceData> villagerResources = new();
    [Export] private GameResourceData gameDataesources;
    public int SaveGame(Array<VillagerResourceData> villagerResources, GameResourceData gameDataesources)
    {
        this.villagerResources = villagerResources;
        this.gameDataesources = gameDataesources;
        ResourceSaver.Save(this, SaveFilePath);
        return 0;
    }

    public Resource LoadGame()
    {
        if (ResourceLoader.Exists(SaveFilePath) && ResourceLoader.HasCached(SaveFilePath))
            return ResourceLoader.Load(SaveFilePath);
        return null;
    }
}
