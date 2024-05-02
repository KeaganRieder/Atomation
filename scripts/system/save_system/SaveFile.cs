namespace Atomation.Resources;

using Atomation.Map;
using Atomation.Player;
using Godot;

public class SaveFile
{
    public int Version { get; set; } = 0;
    public string Name { get; set; }

    public SavedPlayer SavedPlayerData { get; set; }

    public SavedMap SavedMapData { get; set; }

    public SaveFile(){

    }

    public SaveFile(string saveName){
        
    }
}