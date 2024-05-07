namespace Atomation.Map;

using Godot;
using System.Collections.Generic;
using Newtonsoft.Json;

public class SavedMap
{
    public SavedChunk[] SavedChunks;
    public MapData MapSettings;

    public SavedMap(){}

    public SavedMap(SavedChunk[] savedChunks){
        MapSettings = MapData.GetData();

        this.SavedChunks = savedChunks;
    }

    public void load(){
        GD.Print("loading not implemented");
    }

}