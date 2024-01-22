using System;
using Godot;

/// <summary>
/// class which is used by the FileManger to load def files 
/// and cached them to be reference through games runtime
/// </summary>
public class DefResources
{
    public const string TERRAIN_DEFS_PATH = "data/core/defs/terrain/";
    public const string BIOME_DEFS_PATH = "data/core/defs/biomes/";

    public DefDatabase<TerrainNew> TerrainDefs;
    public DefDatabase<Biome> BiomeDefs; //this is a todo still

    public DefResources(){      
    }

    public void LoadResources(){
        GD.Print("loading Terrain Files");
        TerrainDefs = new DefDatabase<TerrainNew>(TERRAIN_DEFS_PATH);
        GD.Print("loading Biome Files");
        BiomeDefs = new DefDatabase<Biome>(BIOME_DEFS_PATH);
    }

    public TerrainNew Terrain(string terrainID){
        return TerrainDefs[terrainID];
    }
    public Biome Biome(string biomeID){
        return BiomeDefs[biomeID];
    }
}