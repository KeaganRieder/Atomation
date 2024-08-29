namespace Atomation.Systems;

using Newtonsoft.Json;
using Atomation.GameMap;
using Atomation.Player;
using Godot;

public class SaveFile
{
    [JsonProperty]
    private int version = 0;
    [JsonProperty]
    private string name;
    [JsonProperty]
    private SavedPlayer savedPlayerData;
    // [JsonProperty]
    // private SavedMap savedMapData;

    [JsonConstructor]
    public SaveFile() { }

    public SaveFile(string saveName)
    {
        name = saveName;
        Save();
    }

    public string GetName()
    {
        return name;
    }
     public int GetVersion()
    {
        return version;
    }

    public void Save()
    {
        // savedPlayerData = Player.Instance.Save();
        // savedMapData = WorldMap.Instance.Save();
    }

    public void Load()
    {
        // Player.Instance.Load(savedPlayerData);
        // WorldMap.Instance.Load(savedMapData);
    }

}