using System;
using Godot;
public class DefFile<datatype>{
    public datatype[] defs;
}
/// <summary>
/// is a collection of def files which pulled at launch and then cached
/// to be later used to create various thing sin the game
/// </summary>
public static class Defs
{
    //file location
    public const string TERRAIN_DEFS_PATH = "data/core/defs/terrain/";
    public const string STAT_DEFS_PATH = "data/core/defs/biomes";
    public const string BIOME_DEFS_PATH = "data/core/defs/biomes";

    //cached dictionary
    public static DefDatabase<Terrain> terrainDefs;
    // private static DefDatabase<BiomeDef> biomeDefs;
    
    public static void LoadDefs(){
        GD.Print($"Reading Defs");
        try
        {
            terrainDefs = new DefDatabase<Terrain>(TERRAIN_DEFS_PATH);
            // biomeDefs = new DefDatabase<BiomeDef>(BIOME_DEFS_PATH); s
        }
        catch (Exception error)
        {
            GD.PrintErr("Error reading failed " + error.Message);
        }
    }
    
    public static Terrain CreatTerrain(string name){
        try
        {
            // return new Terrain(terrainDefs[name]);
            return default;
        }
        catch (Exception error)
        {
            GD.PrintErr("Error failled to create from Def " + error.Message);
            return default;
        }   
    }

}